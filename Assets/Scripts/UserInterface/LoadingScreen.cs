using System;
using System.Collections;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class LoadingScreen: MonoBehaviour {

        [Header("Attributes")]
        public Color LoadingTextColor = new Color(0.94f, 0.94f, 0.94f);
        public AnimationCurve LoadingTextAnimationCurve;
        public float LoadingTextAnimationDuration = 1f;

        [Header("Unity Setup Fields")]
        public Text LoadingText;
        public CanvasGroup EntitiesBackupMessage;
        public CanvasGroup ExitMessage;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private float animationValue;

        private void Start() {
            fetchOptions();
            fetchDefaultPlayConfiguration();

            StartCoroutine(updateEntities());
        }

        private void Update() {
            animationValue = LoadingTextAnimationCurve.Evaluate(Time.time/LoadingTextAnimationDuration);

            updateLoadingText();
        }

        private void fetchOptions() {
            if (FileClient.I.FileExists(Constants.OPTIONS_FILENAME)) {
                State.I.Options = FileClient.I.LoadFromFile<Options>(Constants.OPTIONS_FILENAME);
            } else {
                FileClient.I.SaveToFile(Constants.OPTIONS_FILENAME, State.I.Options);
            }
        }

        private void fetchDefaultPlayConfiguration() {
            if (FileClient.I.FileExists(Constants.PLAY_CONFIGURATION_FILENAME)) {
                State.I.PlayConfiguration =
                    FileClient.I.LoadFromFile<PlayConfiguration>(Constants.PLAY_CONFIGURATION_FILENAME);
            } else {
                FileClient.I.SaveToFile(Constants.PLAY_CONFIGURATION_FILENAME, State.I.PlayConfiguration);
            }
        }

        private IEnumerator updateEntities() {
            var shouldFetchNewEntities = true;
            var errorHasHappened = false;
            if (FileClient.I.FileExists(Constants.ENTITIES_FILENAME)) {
                try {
                    var entitiesJson = FileClient.I.LoadFromFile<EntitiesJson>(Constants.ENTITIES_FILENAME);
                    if (entitiesJson.created.AddDays(7) > DateTime.Now) {
                        shouldFetchNewEntities = false;
                    }
                } catch (Exception e) {
                    errorHasHappened = true;
                    Logger.Error(e);
                }
            }

            if (shouldFetchNewEntities) {
                Logger.Debug("Fetching and saving entities...");
                yield return StartCoroutine(ServerClient.I.DownloadEntities());
                if (ServerClient.I.HasHadError) {
                    errorHasHappened = true;
                } else {
                    Logger.Debug("Fetching and saving entities... Done.");
                }
            }

            // Check if an error has occured, if it has we use old backup
            if (errorHasHappened) {
                EntitiesBackupMessage.gameObject.SetActive(true);

                Logger.Debug("Getting old backup entities from file...");
                try {
                    var backupEntities =
                        FileClient.I.LoadFromFile<EntitiesJson>(Constants.ENTITIES_BACKUP_FILENAME, true);
                    FileClient.I.SaveToFile(Constants.ENTITIES_FILENAME, backupEntities);
                    Logger.Debug("Getting old backup entities from file... Done.");
                } catch (Exception e) {
                    Logger.Error(e);
                }

                yield return new WaitForSeconds(5f);
                EntitiesBackupMessage.gameObject.SetActive(false);
            }

            // Terminate the game if the game was unable to load any entities
            if (!FileClient.I.FileExists(Constants.ENTITIES_FILENAME)) {
                ExitMessage.gameObject.SetActive(true);
                SceneManager.I.ExitGame(10f);
            }

            Logger.Debug("Loading entities...");
            var newEntitiesJson = FileClient.I.LoadFromFile<EntitiesJson>(Constants.ENTITIES_FILENAME);
            State.I.Entities = newEntitiesJson.entities;
            Logger.Debug("Loading entities... Done.");

            // Wait for fading in to complete
            var remainingFadeTime = Constants.SCENE_TRANSITION_TIME - Time.time;
            yield return new WaitForSeconds(remainingFadeTime);

            SceneManager.I.GoTo(Constants.MAIN_MENU);
        }

        private void updateLoadingText() {
            LoadingText.color = LoadingTextColor.WithAlpha(animationValue*0.25f + 0.75f);
            LoadingText.text = "Loading" + getLoadingTextDots();
        }

        private string getLoadingTextDots() {
            var numberOfDots = animationValue < 0.25f ? 0 : animationValue < 0.5f ? 1 : animationValue < 0.75f ? 2 : 3;

            return " .".Repeat(numberOfDots);
        }

    }

}
