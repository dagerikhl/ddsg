using UnityEngine;

namespace DdSG {

    public class AssetSockets: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public Transform AssetSocket1;
        public Transform AssetSocket2;
        public Transform AssetSocket3;
        public Transform AssetSocket4;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        public static Transform[] Sockets;

        // Private and protected members

        private void Awake() {
            Sockets = new Transform[] { AssetSocket1, AssetSocket2, AssetSocket3, AssetSocket4 };
        }

    }

}
