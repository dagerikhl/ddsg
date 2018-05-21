using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace DdSG {

    public class PauseMenu: SingletonBehaviour<PauseMenu> {

        protected PauseMenu() {
        }

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public AnimationCurve FadeCurve;
        public GameObject RestartPrompt;
        public GameObject MainMenuPrompt;
        public GameObject ExitPrompt;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector

        // Private and protected members
        private CanvasGroup canvasGroup;

        private void Awake() {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        [UsedImplicitly]
        public void Pause() {
            // Pause game
            show();
            GameManager.IsPaused = true;
            Time.timeScale = 0f;

            // Fade in menu
            FadeManager.I.Fade(0f, Constants.PAUSE_TRANSITION_TIME, setAlpha, show);
        }

        [UsedImplicitly]
        public void Resume() {
            FadeManager.I.Fade(
                Constants.PAUSE_TRANSITION_TIME,
                0f,
                setAlpha,
                () => {
                    hide();
                    GameManager.IsPaused = false;
                    Time.timeScale = 1f;
                });
        }

        [UsedImplicitly]
        public void Restart() {
            RestartPrompt.SetActive(true);
            hide();
        }

        [UsedImplicitly]
        public void RestartPromptYes() {
            RestartPrompt.SetActive(false);
            SceneManager.I.RestartScene();

            GameManager.IsPaused = false;
            Time.timeScale = 1f;
        }

        [UsedImplicitly]
        public void RestartPromptNo() {
            RestartPrompt.SetActive(false);
            show();
        }

        [UsedImplicitly]
        public void GoToOptionsMenu() {
            StartCoroutine(hideAfterDelay(Constants.SCENE_TRANSITION_TIME));
            SceneManager.I.GoTo(Constants.OPTIONS_MENU);
        }

        [UsedImplicitly]
        public void GoToMainMenu() {
            MainMenuPrompt.SetActive(true);
            hide();
        }

        [UsedImplicitly]
        public void MainMenuPromptYes() {
            MainMenuPrompt.SetActive(false);
            SceneManager.I.GoTo(Constants.MAIN_MENU, false);

            GameManager.IsPaused = false;
            Time.timeScale = 1f;
        }

        [UsedImplicitly]
        public void MainMenuPromptNo() {
            MainMenuPrompt.SetActive(false);
            show();
        }

        [UsedImplicitly]
        public void Exit() {
            ExitPrompt.SetActive(true);
            hide();
        }

        [UsedImplicitly]
        public void ExitPromptYes() {
            ExitPrompt.SetActive(false);
            SceneManager.I.ExitGame();
        }

        [UsedImplicitly]
        public void ExitPromptNo() {
            ExitPrompt.SetActive(false);
            show();
        }

        public IEnumerator ShowAfterDelay(float delay) {
            yield return new WaitForSecondsRealtime(delay);

            show();
        }

        private void show() {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }

        private IEnumerator hideAfterDelay(float delay) {
            yield return new WaitForSecondsRealtime(delay);

            hide();
        }

        private void hide() {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
        }

        private void setAlpha(float value) {
            canvasGroup.alpha = value;
        }

    }

}
