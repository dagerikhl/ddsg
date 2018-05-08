using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class LoadingScreen: MonoBehaviour {

        [Header("Attributes")]
        public Color LoadingTextColor = new Color(0.94f, 0.94f, 0.94f);
        public AnimationCurve LoadingTextAnimationCurve;
        public float LoadingTextAnimationDuration = 1f;

        [Header("Unity Setup Fields")]
        public ServerClient ServerClient;
        public Text LoadingText;

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
            const string filename = "options";

            if (FileClient.I.FileExists(filename)) {
                State.I.Options = FileClient.I.LoadFromFile<Options>(filename);
            } else {
                FileClient.I.SaveToFile(filename, State.I.Options);
            }
        }

        private void fetchDefaultPlayConfiguration() {
            const string filename = "playConfiguration";

            if (FileClient.I.FileExists(filename)) {
                State.I.PlayConfiguration = FileClient.I.LoadFromFile<PlayConfiguration>(filename);
            } else {
                FileClient.I.SaveToFile(filename, State.I.PlayConfiguration);
            }
        }

        private IEnumerator updateEntities() {
            var shouldFetchNewEntities = true;
            if (FileClient.I.FileExists("entities")) {
                var entitiesJson = FileClient.I.LoadFromFile<EntitiesJson>("entities");
                if (entitiesJson.created.AddDays(7) > DateTime.Now) {
                    shouldFetchNewEntities = false;
                }
            }

            if (shouldFetchNewEntities) {
                Logger.Debug("Fetching and saving entities...");
                yield return StartCoroutine(ServerClient.I.DownloadEntities());
                Logger.Debug("Fetching and saving entities... Done.");
            }

            Logger.Debug("Loading entities...");
            var newEntitiesJson = FileClient.I.LoadFromFile<EntitiesJson>("entities");
            State.I.Entities = newEntitiesJson.entities;
            Logger.Debug("Loading entities... Done.");

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
