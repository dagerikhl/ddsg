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
        public static AssetBehaviour[] AssetBehaviours = new AssetBehaviour[4];
        public static Asset[] Assets = new Asset[4];

        // Private and protected members
        private static Transform[] sockets;

        private void Awake() {
            sockets = new[] { AssetSocket1, AssetSocket2, AssetSocket3, AssetSocket4 };
        }

        public void PlaceAssetsOnSockets() {
            for (var i = 0; i < State.I.GameEntities.SDOs.assets.Length; i++) {
                var asset = State.I.GameEntities.SDOs.assets[i];

                AssetBehaviours[i] = UnityHelper.Instantiate(AssetPrefab, sockets[i].position)
                                                .GetComponent<AssetBehaviour>();
                AssetBehaviours[i].Initialize(asset, i);
                Assets[i] = asset;
            }
        }

        public static Transform GetSocketPosition(int index) {
            return sockets[index];
        }

    }

}
