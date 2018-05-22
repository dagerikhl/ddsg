using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class GameManager: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public TextMeshProUGUI GameTimeUi;
        public TextMeshProUGUI DifficultyUi;
        public AssetSockets AssetSockets;

        public GameObject HoverOverlayPrefab;
        public GameObject GhostMitigationPrefab;
        public GameObject EnteredSystemMessagePrefab;

        public GameObject QuickPauseOverlay;
        public Button MenuButton;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]
        public static GameState GameState;

        public static bool IsUiBlocking;
        public static bool IsBuilding;
        public static bool IsGameOver;
        public static bool IsQuickPaused;

        // Private and protected members
        private BuildManager buildManager;

        private float secondsElapsed;
        private float SecondsElapsed {
            get { return secondsElapsed; }
            set {
                secondsElapsed = value;
                GameTimeUi.text = Formatter.TimeFormat(value).Monospaced();
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
            IsQuickPaused = false;
            DifficultyUi.text = State.I.PlayConfiguration.Difficulty.DifficultyFormat().Monospaced();

            // Set timescale according to settings
            Time.timeScale = State.I.PlayConfiguration.GameSpeed;

            buildManager = GetComponent<BuildManager>();

            assignPrefabHelperObjects();

            EntitiesPicker.PickGameEntities();
            AssetSockets.PlaceAssetsOnSockets();
            buildManager.CreateCourseOfActionButtons();
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

            if (Input.GetKeyDown(KeyCode.Space)) {
                QuickPause();
            }
        }

        public static void Win() {
            if (!IsGameOver) {
                IsGameOver = true;
                updateHighscores(GameOverState.Win);

                GameOverMenu.I.Show(GameOverState.Win);
            }
        }

        public static void Lose() {
            if (!IsGameOver) {
                IsGameOver = true;
                updateHighscores(GameOverState.Lose);

                GameOverMenu.I.Show(GameOverState.Lose);
            }
        }

        [UsedImplicitly]
        public void QuickPause() {
            IsQuickPaused = !IsQuickPaused;

            if (IsQuickPaused) {
                Time.timeScale = 0f;
            } else {
                Time.timeScale = State.I.PlayConfiguration.GameSpeed;
            }

            QuickPauseOverlay.SetActive(IsQuickPaused);
            MenuButton.interactable = !IsQuickPaused;
        }

        private void assignPrefabHelperObjects() {
            HelperObjects.HoverOverlayPrefab = HoverOverlayPrefab;
            HelperObjects.GhostMitigationPrefab = GhostMitigationPrefab;
            HelperObjects.EnteredSystemMessagePrefab = EnteredSystemMessagePrefab;
        }

        private static void updateHighscores(GameOverState gameOverState) {
            List<Highscore> highscores = FileClient.I.FileExists(Constants.HIGHSCORES_FILENAME)
                ? FileClient.I.LoadFromFile<Highscore[]>(Constants.HIGHSCORES_FILENAME).ToList()
                : new List<Highscore>();

            var highscore = new Highscore { Time = DateTime.Now, State = gameOverState, Score = PlayerStats.I.Score };
            highscores.Add(highscore);

            FileClient.I.SaveToFile(Constants.HIGHSCORES_FILENAME, highscores.ToArray());
        }

    }

}
