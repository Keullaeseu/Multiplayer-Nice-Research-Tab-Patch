using Multiplayer.Compat;
using Verse;

namespace MultiplayerNiceResearchTabPatch.Source.Mods;

/// <summary>
///     Multiplayer Patch for Nice Research Tab by Andromeda,
///     Last Update: 22 Oct @ 4:34pm 2026
/// </summary>
/// <see href="https://steamcommunity.com/sharedfiles/filedetails/?id=3773496046" />
/// <remarks>
///     Follows the rwmt/Multiplayer-Compatibility pattern: UI actions are
///     Harmony-prefixed and redirected into [MpCompatSyncMethod] workers that
///     only touch game state, so every client executes the same mutation.
///     What is synced:
///     - add to queue (shift-click / queue buttons)
///     - insert at front (alt-click / queue buttons)
///     - restart queue
///     - remove from queue (including dependent projects)
///     - reorder inside the queue
///     What is fixed:
///     - Nice Research Tab's own sync handlers call Apply methods that require
///     ResearchTreeWidget.Instance / MainTabWindow_ResearchTree.Instance, which
///     only exist on clients that have opened the research tab. Remote clients
///     threw (or silently dropped the action). The workers below run the exact
///     same Apply methods against a lightweight, def-based fallback tree (and an
///     uninitialized window placeholder used solely for SetQueueDirty) whenever
///     the real UI is not available.
///     - UI methods that consume RNG (hidden research obfuscation, node chains,
///     animations) are wrapped in Rand.PushState/PopState so the shared RNG
///     stream stays identical across clients.
///     - CombineAnomalyQueues is a per-client mod setting that decides which
///     queue a project is written to. In multiplayer it is normalized to its
///     default (Advanced merged into Basic) on every client so the replicated
///     queue actions cannot land in different queues. Other NRT settings only
///     affect the local layout/rendering.
///     Known limitations (documented, not silently broken):
///     - Research finished letters are created locally by NRT on each client
///     (Multiplayer has no ReceiveLetter sync method); they are not part of
///     the simulation.
///     - Debug Options' AddProgress is not synced by Multiplayer. It is a dev
///     tool and is left untouched.
///     - Other NRT gameplay settings (packer, mod grouping) should match
///     between players; mismatched graphs only affect layout except for the
///     rare FineTune redirect rules.
/// </remarks>
[MpCompatFor("Andromeda.NiceResearchTab")]
public partial class NiceResearchTab
{
    private const string LogPrefix = "[Multiplayer Nice Research Tab Patch]";

    public NiceResearchTab(ModContentPack content)
    {
        LongEventHandler.ExecuteWhenFinished(LatePatch);
    }

    private void LatePatch()
    {
        Log.Message($"{LogPrefix} Initializing...");

        MpCompatPatchLoader.LoadPatch(this);
        PatchRandomMethods();

        Log.Message($"{LogPrefix} Initialized.");
    }
}