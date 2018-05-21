using UnityEngine;

namespace DdSG {

    public static class VectorExtensions {

        public static Vector3 WithNewX(this Vector3 vector, float x) {
            return new Vector3(x, vector.y, vector.z);
        }

        public static Vector3 WithNewY(this Vector3 vector, float y) {
            return new Vector3(vector.x, y, vector.z);
        }

        public static Vector3 WithNewZ(this Vector3 vector, float z) {
            return new Vector3(vector.x, vector.y, z);
        }

    }

}
