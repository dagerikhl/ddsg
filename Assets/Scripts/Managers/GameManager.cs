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

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]
        public static bool IsPaused;
        public static bool ShouldResume;
        public static bool IsGameOver;
        public static bool IsUiBlocking;

        public static bool IsBuilding;

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

            if (!IsPaused && !IsBuilding) {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    FindObjectOfType<PauseMenu>().Pause();
                }
            }
        }

        private void assignPrefabHelperObjects() {
            HelperObjects.HoverOverlayPrefab = HoverOverlayPrefab;
            HelperObjects.GhostMitigationPrefab = GhostMitigationPrefab;
        }

    }

}
