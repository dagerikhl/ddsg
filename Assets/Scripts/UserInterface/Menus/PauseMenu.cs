using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace DdSG {

    public class PauseMenu: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public AnimationCurve FadeCurve;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector

        // Private members
        private CanvasGroup canvasGroup;

        private void Awake() {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        [UsedImplicitly]
        public void Pause() {
            // Deactivate and suspend time
            gameObject.SetActive(true);
            Time.timeScale = 0f;

            // Fade in menu
            StartCoroutine(fadeIn());
        }

        [UsedImplicitly]
        public void Resume() {
            StartCoroutine(fadeOutAndPerformAction());
        }

        [UsedImplicitly]
        public void Restart() {
            StartCoroutine(fadeOutAndPerformAction(SceneManager.I.RestartScene));
        }

        [UsedImplicitly]
        public void GoToOptionsMenu() {
            StartCoroutine(fadeOutAndPerformAction(() => SceneManager.I.GoTo(Constants.OPTIONS_MENU)));
        }

        [UsedImplicitly]
        public void GoToMainMenu() {
            StartCoroutine(fadeOutAndPerformAction(() => SceneManager.I.GoTo(Constants.MAIN_MENU)));
        }

        [UsedImplicitly]
        public void Exit() {
            StartCoroutine(fadeOutAndPerformAction(() => SceneManager.I.ExitGame()));
        }

        private IEnumerator fadeIn() {
            // Fade
            float t = 0f;

            while (t < Constants.PAUSE_TRANSITION_TIME) {
                t += Time.unscaledDeltaTime;
                float alpha = FadeCurve.Evaluate(t/Constants.PAUSE_TRANSITION_TIME);
                canvasGroup.alpha = alpha;

                yield return 0;
            }
        }

        private IEnumerator fadeOutAndPerformAction(Action action = null) {
            // Fade
            float t = Constants.PAUSE_TRANSITION_TIME;

            while (t > 0f) {
                t -= Time.unscaledDeltaTime;
                float alpha = FadeCurve.Evaluate(t/Constants.PAUSE_TRANSITION_TIME);
                canvasGroup.alpha = alpha;

                yield return 0;
            }

            // Hide and resume time
            gameObject.SetActive(false);
            Time.timeScale = 1f;

            // Perform next action if supplied
            if (action != null) {
                action();
            }
        }

    }

}
