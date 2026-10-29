using System.Reflection;
using System.Runtime.Serialization;
using HarmonyLib;
using NiceResearchTab;
using UnityEngine;
using Verse;

namespace MultiplayerNiceResearchTabPatch.Source.Mods;

public partial class NiceResearchTab
{
    private static MainTabWindow_ResearchTree windowPlaceholder;
    private static ResearchTreeWidget fallbackTree;
    private static Game fallbackTreeGame;
    private static MethodInfo worldTechLevelAllowed;

    // NRT's Apply methods dereference both singletons. The window is only used
    // by SetQueueDirty(), so an uninitialized instance is enough (no ctor means
    // no window id allocation). The tree is used for FindByProject/SolveQueue/
    // UpdateNodes, which only depend on def data, so a throwaway def graph is a
    // faithful stand-in until the real graph is built by PreOpen/NodeServer.
    private static void RunQueueAction(Action action, bool ensureTree = true)
    {
        if (ResearchQueue.Instance == null)
            return;

        var previousWindow = MainTabWindow_ResearchTree.Instance;
        var previousTree = ResearchTreeWidget.Instance;
        var previousTab = ResearchTreeWidget.CurrentTab;
        var swapTree = ensureTree &&
                       (previousTree == null || previousTree.Nodes == null || previousTree.Nodes.Count == 0);

        try
        {
            if (previousWindow == null)
                MainTabWindow_ResearchTree.Instance = GetWindowPlaceholder();
            if (swapTree)
                ResearchTreeWidget.Instance = GetFallbackTree();

            action();
        }
        finally
        {
            if (swapTree)
            {
                ResearchTreeWidget.Instance = previousTree;
                ResearchTreeWidget.CurrentTab = previousTab;
            }

            MainTabWindow_ResearchTree.Instance = previousWindow;
        }
    }

    private static MainTabWindow_ResearchTree GetWindowPlaceholder()
    {
        return windowPlaceholder ??=
            (MainTabWindow_ResearchTree)FormatterServices.GetUninitializedObject(typeof(MainTabWindow_ResearchTree));
    }

    private static ResearchTreeWidget GetFallbackTree()
    {
        if (fallbackTree == null || fallbackTreeGame != Current.Game)
        {
            fallbackTree = BuildFallbackTree();
            fallbackTreeGame = Current.Game;
        }

        return fallbackTree;
    }

    // Mirrors NodeServer.GroupProjectsByTab plus LinkSolver.InitializeLinks:
    // all defs visible in the tree (World Tech Level filter applied) and links
    // from prerequisites + hiddenPrerequisites where both nodes exist.
    private static ResearchTreeWidget BuildFallbackTree()
    {
        var tree = new ResearchTreeWidget(Rect.zero);
        var nodes = new Dictionary<ResearchProjectDef, ResearchNode>();

        foreach (var project in DefDatabase<ResearchProjectDef>.AllDefsListForReading)
        {
            if (project == null || !IsProjectInTree(project))
                continue;

            var node = new ResearchNode(
                new ResearchNode.NodeXYSystem(0f, 0f),
                ResearchTreeWidget.NodeWidth,
                ResearchTreeWidget.NodeHeight)
            {
                Project = project,
                Tab = project.tab
            };

            nodes[project] = node;
            tree.Nodes.Add(node);
        }

        foreach (var node in nodes.Values)
        {
            AddLinks(node, node.Project.prerequisites, nodes);
            AddLinks(node, node.Project.hiddenPrerequisites, nodes);
        }

        return tree;
    }

    private static void AddLinks(
        ResearchNode goal,
        List<ResearchProjectDef> prerequisites,
        Dictionary<ResearchProjectDef, ResearchNode> nodes)
    {
        if (prerequisites == null)
            return;

        foreach (var prerequisite in prerequisites)
        {
            if (prerequisite == null || !nodes.TryGetValue(prerequisite, out var from))
                continue;

            var link = new ResearchNodeLink(from, goal);
            from.LinksGoal.Add(link);
            goal.LinksFrom.Add(link);
        }
    }

    private static bool IsProjectInTree(ResearchProjectDef project)
    {
        if (!IsAllowedByWorldTechLevel(project))
            return false;

        return NodeServer.modGrouping ? project.tab != null : NodeServer.RedirectTab(project) != null;
    }

    private static bool IsAllowedByWorldTechLevel(ResearchProjectDef project)
    {
        ModIntegration integration = null;
        foreach (var candidate in ModIntegration.Integrations)
            if (candidate != null && candidate.GetType().Name == "WorldTechLevelIntegration")
            {
                integration = candidate;
                break;
            }

        if (integration == null || !integration.Initialized)
            return true;

        worldTechLevelAllowed ??= AccessTools.Method(integration.GetType(), "Allowed");
        if (worldTechLevelAllowed == null)
            return true;

        try
        {
            return (bool)worldTechLevelAllowed.Invoke(integration, new object[] { project });
        }
        catch (Exception exception)
        {
            Log.ErrorOnce($"{LogPrefix} WorldTechLevelIntegration.Allowed failed: {exception}", 1934820213);
            return true;
        }
    }
}