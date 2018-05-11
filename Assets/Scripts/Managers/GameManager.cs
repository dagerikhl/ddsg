﻿using System.Collections;
using TMPro;
using UnityEngine;

namespace DdSG {

    public class GameManager: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public TextMeshProUGUI GameTime;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        public static bool IsPaused;
        public static bool ShouldResume;

        // Private and protected members
        private float secondsElapsed;

        private void Awake() {
            // TODO Make alterations according to PlayConfiguration

            // TODO Pick out entities

            // TODO Set a real number of assets
            const int numberOfAssets = 4;
            PlayerStats.I.NumberOfAssets = numberOfAssets;

            StartCoroutine(test());
        }

        private IEnumerator test() {
            while (true) {
                PlayerStats.I.SetAssetIntegrity(Mathf.RoundToInt(Random.value*3), Random.value*100);
                yield return new WaitForSeconds(1);
            }
        }

        private void Update() {
            if (!IsPaused) {
                secondsElapsed += Time.deltaTime;

                GameTime.text = TimeHelper.FormatTime(secondsElapsed).Monospaced();
            }
        }

    }

}
