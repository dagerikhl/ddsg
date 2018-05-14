using TMPro;
using UnityEngine;

namespace DdSG {

    public class GameManager: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public TextMeshProUGUI GameTime;
        public GameObject HoverOverlayPrefab;
        public AssetSockets AssetSockets;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]
        public static bool IsPaused;
        public static bool ShouldResume;

        // Private and protected members
        private float secondsElapsed;

        private void Awake() {
            // Fetch entities here if it's a debug build so we can start from game view
            if (Debug.isDebugBuild) {
                var newEntitiesJson = FileClient.I.LoadFromFile<EntitiesJson>(Constants.ENTITIES_FILENAME);
                State.I.Entities = newEntitiesJson.entities;
            }

            HelperObjects.HoverOverlayPrefab = HoverOverlayPrefab;

            EntitiesPicker.PickGameEntities();

            AssetSockets.PlaceAssetsOnSockets();
        }

        private void Update() {
            if (IsPaused) {
                Time.timeScale = 1f;
            } else {
                Time.timeScale = State.I.PlayConfiguration.GameSpeed;

                updateGameTime();
            }

            if (!IsPaused) {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    FindObjectOfType<PauseMenu>().Pause();
                }
            }
        }

        private void updateGameTime() {
            secondsElapsed += Time.unscaledDeltaTime;

            GameTime.text = TimeHelper.FormatTime(secondsElapsed).Monospaced();
        }

    }

}
