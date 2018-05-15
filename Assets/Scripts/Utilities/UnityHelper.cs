using UnityEngine;

namespace DdSG {

    public static class UnityHelper {

        public static T Instantiate<T>(T original) where T : Object {
            return Object.Instantiate(original, Vector3.zero, Quaternion.identity, HelperObjects.Ephemerals);
        }

        public static T Instantiate<T>(T original, Vector3 position) where T : Object {
            return Object.Instantiate(original, position, Quaternion.identity, HelperObjects.Ephemerals);
        }

        public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object {
            return Object.Instantiate(original, position, rotation, HelperObjects.Ephemerals);
        }

    }

}
