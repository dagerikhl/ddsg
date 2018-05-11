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

        // Private and protected members
        private CanvasGroup canvasGroup;

        private void Awake() {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        private void Update() {
            if (GameManager.ShouldResume) {
                StartCoroutine(showAfterDelay(Constants.SCENE_TRANSITION_TIME));
                GameManager.ShouldResume = false;
            }
        }

        [UsedImplicitly]
        public void Pause() {
            // Pause game
            show();
            GameManager.IsPaused = true;

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
            hide();
            SceneManager.I.GoTo(Constants.OPTIONS_MENU);
        }

        [UsedImplicitly]
        public void GoToMainMenu() {
            StartCoroutine(fadeOutAndPerformAction(() => SceneManager.I.GoTo(Constants.MAIN_MENU, false)));
        }

        [UsedImplicitly]
        public void Exit() {
            StartCoroutine(fadeOutAndPerformAction(() => SceneManager.I.ExitGame()));
        }

        private IEnumerator showAfterDelay(float delay) {
            yield return new WaitForSeconds(delay);

            show();
        }

        private void show() {
            // gameObject.SetActive(true);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }

        private void hide() {
            // gameObject.SetActive(false);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
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

            show();
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

            // Hide and potentially resume
            hide();
            GameManager.IsPaused = false;

            // Perform next action if supplied
            if (action != null) {
                action();
            }
        }

    }

}
