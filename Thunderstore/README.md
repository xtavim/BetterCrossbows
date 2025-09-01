# BetterCrossbows

Enhanced crossbow mechanics for Valheim with configurable reload times, movement freedom, and stamina management. Features ServerSync for multiplayer compatibility.

## Features

### 🏹 Crossbow Enhancements

- **Configurable Reload Time**: Adjust crossbow reload speed (1-10 seconds, default 4)
- **Reload While Moving**: Removes movement restrictions, allowing reloading while moving, jumping, or performing other actions
- **Stamina Management**: Option to disable stamina drain during reload

### 🧠 Smart Reload System

- **Movement Freedom**: When enabled, crossbows can reload while moving
- **Stamina Control**: Toggle stamina consumption during reload on/off
- **Immediate Application**: Changes apply instantly to all crossbows

## Configuration

### ServerSync Settings

- **Lock Configuration**: When enabled, only server admins can change settings

### Crossbow Settings

- **Crossbow Enabled**: Main functionality toggle
- **Crossbow Loading Time**: Reload duration in seconds (1-10, less = faster)
- **Reload While Moving**: Remove movement restrictions to allow reloading while moving/jumping
- **Reload Stamina Drain**: Toggle stamina consumption during reload

## How It Works

1. **Reload Time**: Modifies the base reload time for all crossbows
2. **Movement Freedom**: When enabled, reload actions persist even when moving
3. **Stamina Control**: When disabled, reload actions consume no stamina
4. **Crossbow Detection**: Automatically detects all crossbow-type weapons

## Recommended Mods

Pair BetterCrossbows with [SaveCrossbowState by Azumatt](https://thunderstore.io/c/valheim/p/Azumatt/SaveCrossbowState/) for a better crossbow experience. SaveCrossbowState saves crossbow loaded state when switching weapons, preventing unnecessary reloads.

## Changelog

See [CHANGELOG.md](changelog) for version history.

## License

This mod is provided as-is for the Valheim community.
