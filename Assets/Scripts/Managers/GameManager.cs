using TMPro;
using UnityEngine;

namespace DdSG {

    public class GameManager: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public TextMeshProUGUI GameTime;
        public GameObject HoverOverlayPrefab;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        public static bool IsPaused;
        public static bool ShouldResume;

        // Private and protected members
        private float secondsElapsed;

        private void Awake() {
            HelperObjects.HoverOverlay =
                Instantiate(HoverOverlayPrefab, HelperObjects.Ephemerals).GetComponent<HoverOverlay>();

            // TODO Pick out entities

            // TODO Set a real number of assets
            const int numberOfAssets = 4;
            PlayerStats.I.NumberOfAssets = numberOfAssets;
        }

        private void Update() {
            if (IsPaused) {
                Time.timeScale = 1f;
            } else {
                Time.timeScale = State.I.PlayConfiguration.GameSpeed;

                updateGameTime();
            }
        }

        private void updateGameTime() {
            secondsElapsed += Time.deltaTime;

            GameTime.text = TimeHelper.FormatTime(secondsElapsed).Monospaced();
        }

    }

}
