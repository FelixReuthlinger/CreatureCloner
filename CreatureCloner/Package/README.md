# Creature Cloner

Simple mod to clone existing creature prefabs and give the target ones new values for most basic configs, like rename or
set another faction. This mod might get more enhanced to maybe later some time replace the RRR mods, since the author of
those isn't active anymore.

WARNING to all ppl that are going to use this mod and wondering why the creatures won't appear in game: you will still
need to use mods like [SpawnThat](https://valheim.thunderstore.io/package/ASharpPen/Spawn_That/) to make the creatures
appear in game.

## Features

Clone creature prefabs (required to have an Character and Humanoid component implemented, which all vanilla ones at
least apply to). The cloning will remain the original source prefabs chosen untouched and will create another new prefab
with a new name. The new prefab can be used with other mods
like [SpawnThat](https://valheim.thunderstore.io/package/ASharpPen/Spawn_That/).

### Create default configs from game

The mod does provide a custom console command to creature defaults from the game with all existing and loaded creature
prefabs, you can use these as presets for creating your clones.

Just type ```creature_cloner_write_defaults_to_file``` in console and it will write the
file ```org.bepinex.plugins.creature.cloner.defaults.yaml``` to BepInEx config folder.

### Loading your own changes to game

For creating custom configs, you will need to provide file(s) matching the file name
schema ```org.bepinex.plugins.creature.cloner.custom.*.yaml``` somewhere inside (also in subfolders) of the BepInEx
config folder. You can easily provide multiple files, they will all be loaded.

### Config changes supported so far

* name
* group
* faction
* health
* base flags like if it is a boss or tamed creature
* global key to be set on first time killed
* damage modifiers (how resistant the creature is against different forms of damage)
* weapons, armors, ... used (without changing those)

See also this example config:

```yaml
MountainBoar:
  originalPrefabName: Boar
  characterName: 'Icy Boar'
  characterGroup: boar
  faction: MountainMonsters
  health: 200
  characterIsBoss: false
  isTamed: false
  onDefeatSetGlobalKey: ''
  damageModifiers:
    mBlunt: Normal
    mSlash: Normal
    mPierce: Normal
    mChop: Ignore
    mPickaxe: Ignore
    mFire: Normal
    mFrost: Normal
    mLightning: Normal
    mPoison: Normal
    mSpirit: Immune
  defaultItems:
    - boar_base_attack
  randomWeapons: [ ]
  randomArmors: [ ]
  randomShields: [ ]
  randomSets: [ ]
```

### Planned changes

* maybe coloring
* avoiding water, fire, etc.

## Changelog

* 0.1.0 -> initial release

## Contact

* https://github.com/FelixReuthlinger/CreatureCloner
* Discord: Flux#0062 (you can find me around some of the Valheim modding discords, too)
