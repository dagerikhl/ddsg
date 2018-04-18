using UnityEngine;
using UnityEngine.UI;
using UnitySceneManagement = UnityEngine.SceneManagement;
using System.Collections;
using JetBrains.Annotations;

namespace DdSG {

    public class SceneManager: MonoBehaviour {

        [Header("Attributes")]
        public AnimationCurve FadeCurve;

        [Header("Unity Setup Fields")]
        public Image FadeOverlay;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector

        // Private members
        private const float fadeTime = 0.8f;
        private static readonly Color fadeColor = new Color(0.9f, 0.9f, 0.92f);

        private GraphicRaycaster inputBlocker;

        private void Start() {
            inputBlocker = FadeOverlay.GetComponent<GraphicRaycaster>();

            FadeOverlay.color = fadeColor;

            StartCoroutine(fadeIn());
        }

        public void GoTo(string sceneName) {
            // Update last and current scene in state
            State.LastScene = UnitySceneManagement.SceneManager.GetActiveScene().name;
            State.CurrentScene = sceneName;

            // Fade to new scene
            StartCoroutine(fadeOut(sceneName));
        }

        [UsedImplicitly]
        public void GoToLastMenu() {
            GoTo(State.LastScene);
        }

        private IEnumerator fadeIn() {
            // Fade
            float t = fadeTime;

            while (t > 0f) {
                t -= Time.deltaTime;
                float alpha = FadeCurve.Evaluate(t/fadeTime);
                FadeOverlay.color = fadeColor.WithAlpha(alpha);

                yield return 0;
            }

            // Disable input blocking
            inputBlocker.enabled = false;
        }

        private IEnumerator fadeOut(string sceneName) {
            // Enable input blocking
            inputBlocker.enabled = true;

            // Fade
            float t = 0f;

            while (t < fadeTime) {
                t += Time.deltaTime;
                float alpha = FadeCurve.Evaluate(t/fadeTime);
                FadeOverlay.color = fadeColor.WithAlpha(alpha);

                yield return 0;
            }

            // Load new scene
            UnitySceneManagement.SceneManager.LoadScene(sceneName);
        }

    }

}
