﻿using TMPro;
using UnityEngine;

namespace DdSG {

    public class GameManager: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public TextMeshProUGUI GameTime;
        public AssetSockets AssetSockets;

        public GameObject HoverOverlayPrefab;
        public GameObject GhostMitigationPrefab;
        public GameObject EnteredSystemMessagePrefab;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]
        public static GameState GameState;

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
            if (Debug.isDebugBuild && State.I.Entities == null) {
                var newEntitiesJson = FileClient.I.LoadFromFile<EntitiesJson>(Constants.ENTITIES_FILENAME);
                State.I.Entities = newEntitiesJson.entities;
            }

            // Set current running state
            GameState = GameState.Running;

            // Set timescale according to settings
            Time.timeScale = State.I.PlayConfiguration.GameSpeed;

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
                if (GameState == GameState.Running) {
                    if (!IsBuilding) {
                        HelperObjects.PauseMenu.Pause();
                    }
                } else if (GameState == GameState.Paused) {
                    HelperObjects.PauseMenu.Resume();
                }
            }
        }

        private void assignPrefabHelperObjects() {
            HelperObjects.HoverOverlayPrefab = HoverOverlayPrefab;
            HelperObjects.GhostMitigationPrefab = GhostMitigationPrefab;
            HelperObjects.EnteredSystemMessagePrefab = EnteredSystemMessagePrefab;
        }

    }

}
