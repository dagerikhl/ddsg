using UnityEngine;
using UnityEngine.UI;
using UnitySceneManagement = UnityEngine.SceneManagement;
using System.Collections;

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
            FadeManager.I.Fade(
                Constants.SCENE_TRANSITION_TIME,
                0f,
                setFadeOverlayColor,
                () => InputBlocker.enabled = false,
                FadeCurve);
        }

        public void GoTo(string sceneName, bool additive = true) {
            // Update last and current scene in state
            State.I.LastScene = getCurrentSceneName();
            State.I.CurrentScene = sceneName;

            // Fade to new scene
            FadeManager.I.Fade(
                0f,
                Constants.SCENE_TRANSITION_TIME,
                setFadeOverlayColor,
                () => UnitySceneManagement.SceneManager.LoadScene(
                    sceneName,
                    additive ? UnitySceneManagement.LoadSceneMode.Additive
                        : UnitySceneManagement.LoadSceneMode.Single),
                FadeCurve,
                true);

            AmbientManager.I.UpdateAmbient();
        }

        public void GoToLastMenu() {
            // Update last and current scene in state
            State.I.LastScene = getCurrentSceneName();
            State.I.CurrentScene = getLastSceneName();

            // Fade to new scene
            FadeManager.I.Fade(
                0f,
                Constants.SCENE_TRANSITION_TIME,
                setFadeOverlayColor,
                () => UnitySceneManagement.SceneManager.UnloadSceneAsync(State.I.LastScene),
                FadeCurve,
                true);

            // If going back into the game view when the game is paused we should show pause menu
            if (State.I.CurrentScene == Constants.GAME_VIEW && GameManager.IsPaused) {
                GameManager.ShouldResume = true;
            }

            AmbientManager.I.UpdateAmbient();
        }

        public void RestartScene() {
            FadeManager.I.Fade(
                0f,
                Constants.SCENE_TRANSITION_TIME,
                setFadeOverlayColor,
                () => {
                    GameManager.IsPaused = false;
                    UnitySceneManagement.SceneManager.LoadScene(
                        UnitySceneManagement.SceneManager.GetActiveScene().name);
                },
                FadeCurve,
                true);
        }

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

        private void setFadeOverlayColor(float value) {
            FadeOverlay.color = FadeColor.WithAlpha(value);
        }

        private string getCurrentSceneName() {
            var currentSceneIndex = UnitySceneManagement.SceneManager.sceneCount - 1;
            return UnitySceneManagement.SceneManager.GetSceneAt(currentSceneIndex).name;
        }

        private string getLastSceneName() {
            var lastSceneIndex = UnitySceneManagement.SceneManager.sceneCount - 2;
            return UnitySceneManagement.SceneManager.GetSceneAt(lastSceneIndex).name;
        }

    }

}
