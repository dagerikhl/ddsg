using System.Linq;
using TMPro;
using UnityEngine;

namespace DdSG {

    public class PlayerStats: SingletonBehaviour<PlayerStats> {

        [Header("Attributes")]
        public int StartWorth = 50;
        public float StartIntegrity = 100f;

        [Header("Unity Setup Fields")]
        public TextMeshProUGUI WorthUi;
        public TextMeshProUGUI IntegrityUi;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        public int Worth {
            get { return worth; }
            set {
                worth = value;

                WorthUi.text = value.ToString().WorthFormat().Monospaced();
            }
        }
        public int NumberOfAssets {
            get { return numberOfAssets; }
            set {
                numberOfAssets = value;

                integrities = new float[numberOfAssets];
                for (int i = 0; i < numberOfAssets; i++) {
                    integrities[i] = StartIntegrity;
                }
            }
        }

        // Private and protected members
        private int worth;
        private int numberOfAssets;
        private float[] integrities;

        private void Awake() {
            Worth = StartWorth;
        }

        public float GetAssetIntegrity(int assetIndex) {
            return integrities[assetIndex];
        }

        public void SetAssetIntegrity(int assetIndex, float integrity) {
            integrities[assetIndex] = integrity;

            IntegrityUi.text = integrities.Select((e) => e.IntegrityFormat()).Join(" \U0000f142 ").Monospaced();
        }

    }

}
