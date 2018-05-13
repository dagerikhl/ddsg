using UnityEngine;

namespace DdSG {

    public class AssetBehaviour: MonoBehaviour {

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private Asset model;

        public void Initialize(Asset asset) {
            model = asset;
        }

    }

}
