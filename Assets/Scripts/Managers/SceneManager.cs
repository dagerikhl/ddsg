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
        private GraphicRaycaster inputBlocker;

        private void Start() {
            inputBlocker = FadeOverlay.GetComponent<GraphicRaycaster>();

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
            float t = 1f;

            while (t > 0f) {
                t -= Time.deltaTime;
                float alpha = FadeCurve.Evaluate(t);
                FadeOverlay.color = new Color(1f, 1f, 1f, alpha);

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

            while (t < 1f) {
                t += Time.deltaTime;
                float alpha = FadeCurve.Evaluate(t);
                FadeOverlay.color = new Color(1f, 1f, 1f, alpha);

                yield return 0;
            }

            // Load new scene
            UnitySceneManagement.SceneManager.LoadScene(sceneName);
        }

    }

}
