using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace CreatureCloner.Model {
    public class ItemSetsModel {
        [UsedImplicitly] public string SetName;
        [UsedImplicitly] public List<string> ItemNames;

        public static List<ItemSetsModel> FromGameItemSets(Humanoid.ItemSet[] fromGameObjects) {
            return fromGameObjects == null
                ? new List<ItemSetsModel>()
                : fromGameObjects.ToList()
                    .Where(set => set != null)
                    .Select(set => new ItemSetsModel()
                        {SetName = set.m_name, ItemNames = Util.GameObjectToNames(set.m_items)})
                    .ToList();
        }
    }
}