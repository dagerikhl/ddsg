using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace DdSG {

    public class PauseMenu: MonoBehaviour {

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

        private void Update() {
            if (GameManager.ShouldResume) {
                StartCoroutine(showAfterDelay(Constants.SCENE_TRANSITION_TIME));
                GameManager.ShouldResume = false;
            }

            if (GameManager.IsPaused) {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    Resume();
                }
            }
        }

        [UsedImplicitly]
        public void Pause() {
            // Pause game
            show();
            GameManager.IsPaused = true;

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

        private IEnumerator showAfterDelay(float delay) {
            yield return new WaitForSeconds(delay);

            show();
        }

        private void show() {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }

        private IEnumerator hideAfterDelay(float delay) {
            yield return new WaitForSeconds(delay);

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
