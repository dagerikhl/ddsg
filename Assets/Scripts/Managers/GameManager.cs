using TMPro;
using UnityEngine;

namespace DdSG {

    public class GameManager: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public TextMeshProUGUI GameTime;
        public AssetSockets AssetSockets;

        public GameObject HoverOverlayPrefab;
        public GameObject GhostMitigationPrefab;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]
        public static GameState State;

        public static bool IsUiBlocking;
        public static bool IsBuilding;
        public static bool IsGameOver;

        // Private and protected members
        private BuildManager buildManager;

        private float secondsElapsed;
        private float SecondsElapsed {
            get { return secondsElapsed; }
            set {
                secondsElapsed = value;
                GameTime.text = Formatter.TimeFormat(value).Monospaced();
            }
        }

        private void Awake() {
            // Fetch entities here if it's a debug build so we can start from game view
            if (Debug.isDebugBuild && DdSG.State.I.Entities == null) {
                var newEntitiesJson = FileClient.I.LoadFromFile<EntitiesJson>(Constants.ENTITIES_FILENAME);
                DdSG.State.I.Entities = newEntitiesJson.entities;
            }

            State = GameState.Running;

            buildManager = GetComponent<BuildManager>();

            assignPrefabHelperObjects();

            EntitiesPicker.PickGameEntities();
            AssetSockets.PlaceAssetsOnSockets();
            buildManager.CreateCourseOfActionButtons();
        }

        public static void Win() {
            IsGameOver = true;
            Logger.Debug("Game won!");
            Application.Quit();
        }

        private void Update() {
            SecondsElapsed += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (State == GameState.Running) {
                    if (!IsBuilding) {
                        HelperObjects.PauseMenu.Pause();
                    }
                } else if (State == GameState.Paused) {
                    HelperObjects.PauseMenu.Resume();
                }
            }
        }

        private void assignPrefabHelperObjects() {
            HelperObjects.HoverOverlayPrefab = HoverOverlayPrefab;
            HelperObjects.GhostMitigationPrefab = GhostMitigationPrefab;
        }

    }

}
