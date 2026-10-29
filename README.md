# Multiplayer Nice Research Tab Patch

A RimWorld Multiplayer compatibility patch for [Nice Research Tab](https://steamcommunity.com/sharedfiles/filedetails/?id=3773496046).

This mod is designed to improve multiplayer synchronization when playing with the [Nice Research Tab](https://steamcommunity.com/sharedfiles/filedetails/?id=3773496046) mod and [RimWorld Multiplayer](https://steamcommunity.com/sharedfiles/filedetails/?id=2606448745).

## Features

- Synchronizes every Nice Research Tab queue action: add to queue, insert at front, restart queue, remove from queue (including dependent projects), and reorder.
- Runs the mod's own queue logic on every client, even when a player has never opened the research tab, using a def-based fallback research tree and a placeholder window.
- Normalizes the `CombineAnomalyQueues` setting in multiplayer so Advanced projects cannot be written to different queues when players have different settings.
- Isolates Nice Research Tab's UI-only RNG (hidden research labels, node jitter/chains, and animations) with `Rand.PushState`/`Rand.PopState`.
- Mirrors Nice Research Tab's World Tech Level filtering when building the fallback tree.

## Requirements

- RimWorld 1.6
- [Harmony](https://steamcommunity.com/sharedfiles/filedetails/?id=2009463077)
- RimWorld Multiplayer
  - [GitHub version](https://github.com/rwmt/Multiplayer) or [Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=2606448745) version
- [MilkyWay](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562)
- [Multiplayer Milky Way Patch](https://github.com/Keullaeseu/Multiplayer-Milky-Way-Patch/releases/latest)
  — required: covers the MilkyWay `TextUtils` obfuscation RNG used by Nice Research Tab.
- [Nice Research Tab](https://steamcommunity.com/sharedfiles/filedetails/?id=3773496046)

The host and every connected player must use compatible versions of all required mods.

## Installation

### Steam Workshop Dependencies

Subscribe to Harmony, RimWorld Multiplayer, MilkyWay, and Nice Research Tab on the Steam Workshop. Install the Multiplayer Milky Way Patch and this patch from their GitHub releases.

### Manual Installation

1. Download the latest release from the [**Releases**](https://github.com/Keullaeseu/Multiplayer-Nice-Research-Tab-Patch/releases/latest) section.
2. Extract the mod folder into your RimWorld `Mods` directory.
3. Enable the required mods in RimWorld.
4. Use the recommended load order listed below.
5. Make sure every multiplayer player has the same mod list, configuration, and load order.

### Recommended Load Order

1. Harmony
2. Core
3. Royalty, Ideology, Biotech, and Anomaly, if applicable
4. RimWorld Multiplayer
5. [MilkyWay](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562)
6. [Multiplayer Milky Way Patch](https://github.com/Keullaeseu/Multiplayer-Milky-Way-Patch/releases/latest)
7. [Nice Research Tab](https://steamcommunity.com/sharedfiles/filedetails/?id=3773496046)
8. [Multiplayer Nice Research Tab Patch](https://github.com/Keullaeseu/Multiplayer-Nice-Research-Tab-Patch/releases/latest)

The patch must load after RimWorld Multiplayer, MilkyWay, the Multiplayer Milky Way Patch, and Nice Research Tab.

## Multiplayer Usage

All players should have the following mods installed and enabled:

- RimWorld Multiplayer
- [MilkyWay](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562)
- [Multiplayer Milky Way Patch](https://github.com/Keullaeseu/Multiplayer-Milky-Way-Patch/releases/latest)
- [Nice Research Tab](https://steamcommunity.com/sharedfiles/filedetails/?id=3773496046)
- [Multiplayer Nice Research Tab Patch](https://github.com/Keullaeseu/Multiplayer-Nice-Research-Tab-Patch/releases/latest)
- All other required Nice Research Tab dependencies

The host and all connected clients should use the same:

- RimWorld version
- RimWorld Multiplayer version
- MilkyWay version
- Multiplayer Milky Way Patch version
- Nice Research Tab version
- Multiplayer Nice Research Tab Patch version
- World Tech Level settings, if World Tech Level is installed
- Mod configuration
- Mod load order

Do not add, remove, update, or reorder mods while players are connected to the same multiplayer session.

## Compatibility

This patch is intended to provide multiplayer compatibility for [Nice Research Tab](https://steamcommunity.com/sharedfiles/filedetails/?id=3773496046)
itself (queue actions, clients without the research tab open, UI RNG isolation, and
anomaly queue normalization).

The MilkyWay `TextUtils` RNG used by Nice Research Tab is covered by the required
Multiplayer Milky Way Patch listed above, not duplicated here.

Optional Nice Research Tab integrations (World Tech Level, SemiRandom Research,
Discoveries, Vanilla Gravship Expanded/Gravtech, and Medieval Overhaul) are supported
as long as every player has the same mods and settings.

It does not replace:

- [RimWorld Multiplayer](https://steamcommunity.com/sharedfiles/filedetails/?id=2606448745)
- [MilkyWay](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562)
- [Multiplayer Milky Way Patch](https://github.com/Keullaeseu/Multiplayer-Milky-Way-Patch/releases/latest)
- [Nice Research Tab](https://steamcommunity.com/sharedfiles/filedetails/?id=3773496046)

## Known Limitations

- Research-finished letters are created locally by Nice Research Tab on each client and are not part of the synchronized simulation.
- Debug Options' AddProgress is not synchronized by RimWorld Multiplayer. It is a development tool and is left untouched.
- `CombineAnomalyQueues` is normalized to the combined behavior in multiplayer, so a player's local split-queue preference is ignored while playing multiplayer.
- Other Nice Research Tab layout settings (packer, mod grouping) and World Tech Level settings should match between players. Mismatched graph settings can affect which projects appear in the tree.
- Compatibility may be affected by future RimWorld, RimWorld Multiplayer, MilkyWay, or Nice Research Tab updates.

## Credits

- [RimWorld Multiplayer on GitHub](https://github.com/rwmt/Multiplayer)
- [RimWorld Multiplayer on Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=2606448745)
- [Nice Research Tab on Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=3773496046)
- [MilkyWay on Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=3773448562)
- [Multiplayer Milky Way Patch](https://github.com/Keullaeseu/Multiplayer-Milky-Way-Patch)
- [Harmony](https://steamcommunity.com/sharedfiles/filedetails/?id=2009463077)
- [Keullaeseu](https://github.com/Keullaeseu)
