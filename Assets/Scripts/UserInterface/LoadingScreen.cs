using System;
using System.Collections;
using System.Globalization;
using System.Linq;
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
        private bool shouldAnimate = true;
        private float animationValue;

        private void Start() {
            /**
             * TODO Startup cycle
             *
             * 1. Animate loading text
             * 2. Start loading entities
             * 3. Finish loading entities
             * 4. Stop showing loading
             * 5. Fade to Main Menu
             */

            // 2
            StartCoroutine(updateEntities());
        }

        private void Update() {
            animationValue += Time.deltaTime;
            animationValue = LoadingTextAnimationCurve.Evaluate(animationValue/LoadingTextAnimationDuration);

            // 1
            if (shouldAnimate || animationValue < 1f) {
                updateLoadingText();

                if (shouldAnimate) {
                    animationValue = animationValue%1f;
                }
            }
        }

        private IEnumerator updateEntities() {
            Debug.Log("Fetching and saving entities...");
            yield return StartCoroutine(ServerClient.I.DownloadEntities());
            Debug.Log("Fetching and saving entities... Done.");

            // 3
            Debug.Log("Loading entities...");
            var entities = FileClient.I.LoadFromFile<EntitiesJson>("entities.json");
            Debug.Log("Loading entities... Done.");
            // TODO Remove this debug sentence
            Debug.Log(entities.ToString());

            // 4
            shouldAnimate = false;

            // 5
            // TODO Re-enable this
            // SceneManager.I.GoTo(Constants.MAIN_MENU);
        }

        private void updateLoadingText() {
            LoadingText.color = LoadingTextColor.WithAlpha(animationValue);
            LoadingText.text = "Loading" + getLoadingTextDots();
        }

        private string getLoadingTextDots() {
            var numberOfDots = animationValue < 0.25f ? 0 : animationValue < 0.5f ? 1 : animationValue < 0.75f ? 2 : 3;

            return StringExtensions.Repeat(" .", numberOfDots);
        }

    }

}
