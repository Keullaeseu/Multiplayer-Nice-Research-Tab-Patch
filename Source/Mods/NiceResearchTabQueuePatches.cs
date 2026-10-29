using Multiplayer.API;
using Multiplayer.Compat;
using NiceResearchTab;
using Verse;

namespace MultiplayerNiceResearchTabPatch.Source.Mods;

public partial class NiceResearchTab
{
    [MpCompatPrefix(typeof(ResearchQueue), "AddToQueue")]
    private static bool Prefix_AddToQueue(ResearchProjectDef project)
    {
        if (!MP.IsInMultiplayer || !MP.InInterface || MP.IsExecutingSyncCommand)
            return true;

        SyncedAddToQueue(project);
        return false;
    }

    [MpCompatPrefix(typeof(ResearchQueue), "InsertToFront")]
    private static bool Prefix_InsertToFront(ResearchProjectDef project)
    {
        if (!MP.IsInMultiplayer || !MP.InInterface || MP.IsExecutingSyncCommand)
            return true;

        SyncedInsertToFront(project);
        return false;
    }

    [MpCompatPrefix(typeof(ResearchQueue), "RestartQueue")]
    private static bool Prefix_RestartQueue(ResearchProjectDef project)
    {
        if (!MP.IsInMultiplayer || !MP.InInterface || MP.IsExecutingSyncCommand)
            return true;

        SyncedRestartQueue(project);
        return false;
    }

    [MpCompatPrefix(typeof(ResearchQueue), "RemoveFromQueue")]
    private static bool Prefix_RemoveFromQueue(ResearchProjectDef project)
    {
        if (!MP.IsInMultiplayer || !MP.InInterface || MP.IsExecutingSyncCommand)
            return true;

        SyncedRemoveFromQueue(project);
        return false;
    }

    [MpCompatPrefix(typeof(ResearchQueue), "ReorderToIndex")]
    private static bool Prefix_ReorderToIndex(ResearchProjectDef project, int newIndex)
    {
        if (!MP.IsInMultiplayer || !MP.InInterface || MP.IsExecutingSyncCommand)
            return true;

        SyncedReorderToIndex(project, newIndex);
        return false;
    }
}