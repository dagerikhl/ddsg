using System.Linq;
using UnityEngine;

namespace DdSG {

    public static class TransformExtensions {

        public static Transform Clear(this Transform transform) {
            foreach (Transform child in transform.Cast<Transform>().ToList()) {
                Object.Destroy(child.gameObject);
            }

            return transform;
        }

        public static Transform TransferChildrenTo(this Transform transform, Transform newParent) {
            foreach (Transform child in transform.Cast<Transform>().ToList()) {
                child.SetParent(newParent);
            }

            return transform;
        }

    }

}
