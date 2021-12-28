using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Jotunn.Logger;

namespace CreatureCloner.Model {
    public class CreatureModel {
        [UsedImplicitly] public string OriginalPrefabName;

        [UsedImplicitly] public string CharacterName;
        [UsedImplicitly] public string CharacterGroup;
        [UsedImplicitly] public Character.Faction Faction;
        [UsedImplicitly] public float Health;
        [UsedImplicitly] public bool CharacterIsBoss;
        [UsedImplicitly] public bool IsTamed;
        [UsedImplicitly] public string OnDefeatSetGlobalKey;
        [UsedImplicitly] public HitData.DamageModifiers DamageModifiers;

        [UsedImplicitly] public List<string> DefaultItems;
        [UsedImplicitly] public List<string> RandomWeapons;
        [UsedImplicitly] public List<string> RandomArmors;
        [UsedImplicitly] public List<string> RandomShields;
        [UsedImplicitly] public List<ItemSetsModel> RandomSets;

        [UsedImplicitly] public float Scale;

        [CanBeNull]
        public static CreatureModel ReadFromGameObject([CanBeNull] GameObject fromGameObject) {
            if (fromGameObject == null) return null;
            Character character = null;
            Humanoid humanoid = null;
            if (fromGameObject != null && !fromGameObject.TryGetComponent(out character)) {
                Logger.LogError($"prefab doesn't have a 'Character' component, cannot use for cloning");
                return null;
            }

            if (fromGameObject != null && !fromGameObject.TryGetComponent(out humanoid)) {
                Logger.LogError($"prefab doesn't have a 'Humanoid' component, cannot use for cloning");
                return null;
            }

            if (humanoid != null && character != null) {
                return new CreatureModel() {
                    // prefab specifics
                    OriginalPrefabName = fromGameObject.name,

                    // character specifics
                    CharacterName = character.m_name,
                    CharacterGroup = character.m_group,
                    Faction = character.m_faction,
                    Health = character.m_health,
                    CharacterIsBoss = character.m_boss,
                    IsTamed = character.m_tamed,
                    OnDefeatSetGlobalKey = character.m_defeatSetGlobalKey,
                    DamageModifiers = character.m_damageModifiers,

                    // humanoid specifics
                    DefaultItems = Util.GameObjectToNames(humanoid.m_defaultItems),
                    RandomWeapons = Util.GameObjectToNames(humanoid.m_randomWeapon),
                    RandomArmors = Util.GameObjectToNames(humanoid.m_randomArmor),
                    RandomShields = Util.GameObjectToNames(humanoid.m_randomShield),
                    RandomSets = ItemSetsModel.FromGameItemSets(humanoid.m_randomSets),

                    // scale specifics
                    Scale = 1f
                };
            }

            return null;
        }

        public void RegisterCreature(string newPrefabName) {
            Logger.LogInfo($"registered new (cloned) creature with new prefab name '{newPrefabName}'");
        }
    }
}