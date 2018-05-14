using UnityEngine;

namespace DdSG {

    public static class HelperObjects {

        public static Transform Ephemerals { get { return GameObject.FindWithTag("Ephemerals").transform; } }
        public static GameObject HoverOverlayPrefab { get; set; }

    }

}
