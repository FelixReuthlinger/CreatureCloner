using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CreatureCloner.Model {

    public class Util {
        public static List<string> GameObjectToNames(GameObject[] objects) {
            if (objects == null) return new List<string>();
            return objects.ToList()
                .Where(gameObject => gameObject != null)
                .Select(gameObject => gameObject.name)
                .ToList();
        }
    }
}