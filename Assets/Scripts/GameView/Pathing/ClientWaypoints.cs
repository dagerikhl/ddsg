using UnityEngine;

namespace DdSG {

    public class ClientWaypoints: MonoBehaviour {

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        public static Transform[] Points;

        // Private and protected members

        private void Awake() {
            Points = new Transform[transform.childCount];
            for (var i = 0; i < Points.Length; i++) {
                Points[i] = transform.GetChild(i);
            }
        }

    }

}
