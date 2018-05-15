using UnityEngine;

namespace DdSG {

    public class AssetSockets: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public Transform AssetSocket1;
        public Transform AssetSocket2;
        public Transform AssetSocket3;
        public Transform AssetSocket4;
        public GameObject AssetPrefab;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private Transform[] sockets;
        private AssetBehaviour[] assets = new AssetBehaviour[4];

        private void Awake() {
            sockets = new[] { AssetSocket1, AssetSocket2, AssetSocket3, AssetSocket4 };
        }

        public void PlaceAssetsOnSockets() {
            for (var i = 0; i < State.I.GameEntities.SDOs.assets.Length; i++) {
                var asset = State.I.GameEntities.SDOs.assets[i];

                assets[i] = instantiateAsset(asset, i, sockets[i].position);
            }
        }

        private AssetBehaviour instantiateAsset(Asset asset, int assetIndex, Vector3 position) {
            var assetGameObject = Instantiate(AssetPrefab, position, Quaternion.identity, HelperObjects.Ephemerals)
                .GetComponent<AssetBehaviour>();
            assetGameObject.Initialize(asset, assetIndex);

            return assetGameObject;
        }

    }

}
