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
            gameObject.SetActive(true);
            Time.timeScale = 0f;
            StartCoroutine(fadeIn());
        }

        [UsedImplicitly]
        public void Resume() {
            StartCoroutine(fadeOutAndResume());
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

        private IEnumerator fadeOutAndResume() {
            // Fade
            float t = Constants.PAUSE_TRANSITION_TIME;

            while (t > 0f) {
                t -= Time.unscaledDeltaTime;
                float alpha = FadeCurve.Evaluate(t/Constants.PAUSE_TRANSITION_TIME);
                canvasGroup.alpha = alpha;

                yield return 0;
            }

            // Hide
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

    }

}
