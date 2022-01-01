using System.Collections.Generic;
using System.Linq;
using Jotunn.Managers;
using UnityEngine;

namespace CreatureCloner.Model {
    public static class Util {
        public static List<string> GameObjectToNames(GameObject[] objects) {
            if (objects == null) return new List<string>();
            return objects.ToList()
                .Where(gameObject => gameObject != null)
                .Select(gameObject => gameObject.name)
                .ToList();
        }

        public static GameObject[] GameObjectsFromNames(List<string> names) {
            return names
                .Where(name => name != null)
                .Select(name => PrefabManager.Instance.GetPrefab(name))
                .ToArray();
        }
    }
}