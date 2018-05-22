using System.Linq;
using TMPro;
using UnityEngine;

namespace DdSG {

    public class PlayerStats: SingletonBehaviour<PlayerStats> {

        [Header("Attributes")]
        public int StartScore;
        public int StartWorth = 50;
        public float StartIntegrity = 100f;

        [Header("Unity Setup Fields")]
        public TextMeshProUGUI ScoreUi;
        public TextMeshProUGUI WorthUi;
        public TextMeshProUGUI IntegrityUi;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]
        public int Score {
            get { return score; }
            set {
                score = value;

                ScoreUi.text = value.ToString().ScoreFormat().Monospaced();
            }
        }
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
        private int score;
        private int worth;
        private int numberOfAssets;

        private void Awake() {
            Score = StartScore;
            Worth = StartWorth;
        }

        public float GetAssetIntegrity(int assetIndex) {
            return Integrities[assetIndex];
        }

        public void SetAssetIntegrity(int assetIndex, float integrity) {
            Integrities[assetIndex] = integrity;

            if (Integrities.All((i) => i <= 0f)) {
                GameManager.Lose();
            }

            updateIntegrityTextUi();
        }

        public void UpdateStatsForKilledAttack(int value) {
            Score += Mathf.CeilToInt(value*1000f*State.I.PlayConfiguration.Difficulty);
            Worth += Mathf.CeilToInt(value/State.I.PlayConfiguration.Difficulty);
        }

        public void UpdateStatsForLostAsset(int value) {
            Score -= Mathf.FloorToInt(value*2000f/State.I.PlayConfiguration.Difficulty);
        }

        private void updateIntegrityTextUi() {
            IntegrityUi.text = Integrities.Select((e) => e.IntegrityFormat()).Join(" \U0000f142 ").Monospaced();
        }

    }

}
