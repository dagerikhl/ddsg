﻿using System.Linq;
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
        //[HideInInspector]
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

                Integrities = new float[numberOfAssets];
                for (int i = 0; i < numberOfAssets; i++) {
                    Integrities[i] = StartIntegrity;
                }

                updateIntegrityTextUi();
            }
        }
        public float[] Integrities { get; set; }
        public int Waves { get; set; }

        // Private and protected members
        private int worth;
        private int numberOfAssets;

        private void Awake() {
            Worth = StartWorth;
        }

        public float GetAssetIntegrity(int assetIndex) {
            return Integrities[assetIndex];
        }

        public void SetAssetIntegrity(int assetIndex, float integrity) {
            Integrities[assetIndex] = integrity;

            updateIntegrityTextUi();
        }

        private void updateIntegrityTextUi() {
            IntegrityUi.text = Integrities.Select((e) => e.IntegrityFormat()).Join(" \U0000f142 ").Monospaced();
        }

    }

}
