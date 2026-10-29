using Multiplayer.Compat;
using NiceResearchTab;
using Verse;

namespace MultiplayerNiceResearchTabPatch.Source.Mods;

public partial class NiceResearchTab
{
    [MpCompatSyncMethod]
    private static void SyncedAddToQueue(ResearchProjectDef project)
    {
        RunQueueAction(() => ResearchQueue.Instance?.ApplyAddToQueue(project));
    }

    [MpCompatSyncMethod]
    private static void SyncedInsertToFront(ResearchProjectDef project)
    {
        RunQueueAction(() => ResearchQueue.Instance?.ApplyInsertToFront(project));
    }

    [MpCompatSyncMethod]
    private static void SyncedRestartQueue(ResearchProjectDef project)
    {
        RunQueueAction(() => ResearchQueue.Instance?.ApplyRestartQueue(project));
    }

    [MpCompatSyncMethod]
    private static void SyncedRemoveFromQueue(ResearchProjectDef project)
    {
        // ApplyRemoveFromQueue doesn't need the tree, only SetQueueDirty().
        RunQueueAction(() => ResearchQueue.Instance?.ApplyRemoveFromQueue(project), false);
    }

    [MpCompatSyncMethod]
    private static void SyncedReorderToIndex(ResearchProjectDef project, int newIndex)
    {
        // ApplyReorderToIndex only uses the window through ?. and never touches
        // the tree, so it is already safe on clients without the tab open.
        ResearchQueue.Instance?.ApplyReorderToIndex(project, newIndex);
    }
}