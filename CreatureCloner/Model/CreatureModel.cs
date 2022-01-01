using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Jotunn.Managers;
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
                    RandomSets = ItemSetsModel.FromGameItemSets(humanoid.m_randomSets)
                };
            }

            return null;
        }

        public void RegisterCreature(string newPrefabName) {
            if (newPrefabName == null || OriginalPrefabName == null) {
                Logger.LogError(
                    $"prefab cloner information is missing a new name or original name, cannot clone creature");
                return;
            }

            // clone the object from existing creature
            GameObject clonedCreature = PrefabManager.Instance
                .CreateClonedPrefab(newPrefabName, OriginalPrefabName);
            if (!clonedCreature.TryGetComponent(out Humanoid humanoid)) {
                Logger.LogError(
                    $"chosen original prefab '{OriginalPrefabName}' for creature doesn't have a 'Humanoid' " +
                    $"component, cannot use this prefab to clone it");
                return;
            }

            if (!clonedCreature.TryGetComponent(out Character character)) {
                Logger.LogError(
                    $"chosen original prefab '{OriginalPrefabName}' for creature doesn't have a 'Character' " +
                    $"component, cannot use this prefab to clone it");
                return;
            }

            character.m_name = CharacterName;
            character.m_group = CharacterGroup;
            character.m_faction = Faction;
            character.m_health = Health;
            character.m_boss = CharacterIsBoss;
            character.m_tamed = IsTamed;
            character.m_defeatSetGlobalKey = OnDefeatSetGlobalKey;
            character.m_damageModifiers = DamageModifiers;

            humanoid.m_defaultItems = Util.GameObjectsFromNames(DefaultItems);
            humanoid.m_randomWeapon = Util.GameObjectsFromNames(RandomWeapons);
            humanoid.m_randomArmor = Util.GameObjectsFromNames(RandomArmors);
            humanoid.m_randomShield = Util.GameObjectsFromNames(RandomShields);
            humanoid.m_randomSets = RandomSets
                .Where(set => set != null)
                .Select(set => new Humanoid.ItemSet()
                    {m_name = set.SetName, m_items = Util.GameObjectsFromNames(set.ItemNames)}).ToArray();

            PrefabManager.Instance.AddPrefab(clonedCreature);
            PrefabManager.Instance.RegisterToZNetScene(clonedCreature);
            
            Logger.LogInfo($"registered new (cloned) creature with new prefab name '{clonedCreature.name}'");
        }
    }
}