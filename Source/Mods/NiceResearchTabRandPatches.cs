using System.Reflection;
using HarmonyLib;
using Multiplayer.Compat;
using NiceResearchTab;
using Verse;

namespace MultiplayerNiceResearchTabPatch.Source.Mods;

public partial class NiceResearchTab
{
    private static void PatchRandomMethods()
    {
        var methods = new MethodBase[]
        {
            AccessTools.DeclaredMethod(typeof(MainTabWindow_ResearchTree), "CreateFakeUnlocks"),
            AccessTools.DeclaredMethod(typeof(ProjectHeader), "Cache"),
            AccessTools.DeclaredConstructor(typeof(ProjectUnlocks),
                [typeof(TaggedString), typeof(List<Def>), typeof(bool), typeof(bool)]),
            AccessTools.DeclaredMethod(typeof(ResearchNode), "DrawChains"),
            AccessTools.DeclaredMethod(typeof(Settings), "DrawAnimation"),
            AccessTools.DeclaredMethod(typeof(StartResearchButton), "Draw"),
            AccessTools.DeclaredMethod(typeof(ZoomableCanvas), "DrawAnimation")
        };

        PatchingUtilities.PatchPushPopRand(methods);
    }
}