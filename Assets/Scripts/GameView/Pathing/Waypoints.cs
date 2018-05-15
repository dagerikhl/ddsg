using UnityEngine;

namespace DdSG {

    public class Waypoints: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public PathCategory Category;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        public Transform[] Points;

        // Private and protected members

        protected void Awake() {
            Points = new Transform[transform.childCount];
            for (var i = 0; i < Points.Length; i++) {
                Points[i] = transform.GetChild(i);
            }
        }

    }

}
