using UnityEngine;
using UnityEngine.UI;
using UnitySceneManagement = UnityEngine.SceneManagement;
using System.Collections;

namespace DdSG {

    public class SceneManager: ScenePersistentSingleton<SceneManager> {

        protected SceneManager() {
        }

        [Header("Attributes")]
        public Color FadeColor = new Color(0.9f, 0.9f, 0.92f);
        public AnimationCurve FadeCurve;

        [Header("Unity Setup Fields")]
        public Image FadeOverlay;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        protected override string persistentTag { get { return "SceneManager"; } }

        private GraphicRaycaster inputBlocker;

        private void Start() {
            inputBlocker = FadeOverlay.GetComponent<GraphicRaycaster>();

            FadeOverlay.color = FadeColor;

            StartCoroutine(fadeIn());
        }

        public void GoTo(string sceneName) {
            // Update last and current scene in state
            State.I.LastScene = UnitySceneManagement.SceneManager.GetActiveScene().name;
            State.I.CurrentScene = sceneName;

            // Fade to new scene
            StartCoroutine(fadeOut(sceneName));
        }

        private IEnumerator fadeIn() {
            // Fade
            float t = Constants.SCENE_TRANSITION_TIME;

            while (t > 0f) {
                t -= Time.deltaTime;
                float alpha = FadeCurve.Evaluate(t/Constants.SCENE_TRANSITION_TIME);
                FadeOverlay.color = FadeColor.WithAlpha(alpha);

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

            while (t < Constants.SCENE_TRANSITION_TIME) {
                t += Time.deltaTime;
                float alpha = FadeCurve.Evaluate(t/Constants.SCENE_TRANSITION_TIME);
                FadeOverlay.color = FadeColor.WithAlpha(alpha);

                yield return 0;
            }

            // Fade back in
            StartCoroutine(fadeIn());

            // Load new scene
            UnitySceneManagement.SceneManager.LoadScene(sceneName);
        }

    }

}
