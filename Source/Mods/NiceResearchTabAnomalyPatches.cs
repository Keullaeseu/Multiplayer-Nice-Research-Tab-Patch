using Multiplayer.API;
using Multiplayer.Compat;
using NiceResearchTab;
using RimWorld;

namespace MultiplayerNiceResearchTabPatch.Source.Mods;

public partial class NiceResearchTab
{
    // CombineAnomalyQueues is a per-client setting, but it decides whether an
    // Advanced project is stored in the Basic queue or in its own queue. If two
    // players have different values, the same replicated action mutates
    // different lists and the queues desync. Normalize to the default behavior
    // in multiplayer instead of reading the local setting.
    [MpCompatPostfix(typeof(ResearchQueue), "RedirectKnowledgeCategory")]
    private static void Post_RedirectKnowledgeCategory(KnowledgeCategoryDef category, ref KnowledgeCategoryDef __result)
    {
        if (!MP.IsInMultiplayer)
            return;

        __result = category == KnowledgeCategoryDefOf.Advanced ? KnowledgeCategoryDefOf.Basic : category;
    }
}