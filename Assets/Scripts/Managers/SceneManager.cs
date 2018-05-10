using System;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManagement = UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

namespace DdSG {

    public class SceneManager: PersistentSingletonBehaviour<SceneManager> {

        protected SceneManager() {
        }

        [Header("Attributes")]
        public Color FadeColor = new Color(0.9f, 0.9f, 0.92f);
        public AnimationCurve FadeCurve;

        [Header("Unity Setup Fields")]
        public Image FadeOverlay;
        public GraphicRaycaster InputBlocker;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        protected override string persistentTag { get { return "SceneManager"; } }

        private void Start() {
            StartCoroutine(fadeIn());
        }

        public void GoTo(string sceneName, bool additive = true) {
            // Update last and current scene in state
            State.I.LastScene = getCurrentSceneName();
            State.I.CurrentScene = sceneName;

            // Fade to new scene
            StartCoroutine(
                fadeOut(
                    () => UnitySceneManagement.SceneManager.LoadScene(
                        sceneName,
                        additive ? UnitySceneManagement.LoadSceneMode.Additive
                            : UnitySceneManagement.LoadSceneMode.Single)));
        }

        public void GoToLastMenu() {
            // Update last and current scene in state
            State.I.LastScene = getCurrentSceneName();
            State.I.CurrentScene = getLastSceneName();

            Debug.LogError(UnitySceneManagement.SceneManager.sceneCount);
            Debug.LogError(
                string.Join(", ", UnitySceneManagement.SceneManager.GetAllScenes().Select((e) => e.name).ToArray()));

            // Fade to new scene
            StartCoroutine(fadeOut(() => UnitySceneManagement.SceneManager.UnloadSceneAsync(State.I.LastScene)));
        }

        // ReSharper disable once MemberCanBeMadeStatic.Global
        public void RestartScene() {
            UnitySceneManagement.SceneManager.LoadScene(UnitySceneManagement.SceneManager.GetActiveScene().name);
        }

        // ReSharper disable once MemberCanBeMadeStatic.Global
        public void ExitGame(float delay = 0f) {
            StartCoroutine(exitGameRoutine(delay));
        }

        private IEnumerator exitGameRoutine(float delay) {
            if (delay > 0f) {
                yield return new WaitForSeconds(delay);
            }

            Logger.Debug("Exiting game.");
            Application.Quit();
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
            InputBlocker.enabled = false;
        }

        private IEnumerator fadeOut(Action action) {
            // Enable input blocking
            InputBlocker.enabled = true;

            // Fade
            float t = 0f;

            while (t < Constants.SCENE_TRANSITION_TIME) {
                t += Time.deltaTime;
                float alpha = FadeCurve.Evaluate(t/Constants.SCENE_TRANSITION_TIME);
                FadeOverlay.color = FadeColor.WithAlpha(alpha);

                yield return 0;
            }

            // Perform action if supplied
            if (action != null) {
                action();
            }

            // Fade back in
            StartCoroutine(fadeIn());
        }

        private string getCurrentSceneName() {
            var currentSceneIndex = UnitySceneManagement.SceneManager.sceneCount - 1;
            return UnitySceneManagement.SceneManager.GetSceneAt(currentSceneIndex).name;
        }

        private string getLastSceneName() {
            var lastSceneIndex = UnitySceneManagement.SceneManager.sceneCount - 1;
            return UnitySceneManagement.SceneManager.GetSceneAt(lastSceneIndex).name;
        }

    }

}
