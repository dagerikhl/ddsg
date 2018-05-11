﻿using TMPro;
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

        private void Update() {
            if (!IsPaused) {
                secondsElapsed += Time.deltaTime;

                GameTime.text = TimeHelper.FormatTime(secondsElapsed).Monospaced();
            }
        }

    }

}