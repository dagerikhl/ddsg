using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace DdSG {

    public class GameOverMenu: SingletonBehaviour<GameOverMenu> {

        protected GameOverMenu() {
        }

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public AnimationCurve FadeCurve;

        public TextMeshProUGUI TitleText;
        public TextMeshProUGUI ScoreText;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector

        // Private and protected members
        private CanvasGroup canvasGroup;

        private void Awake() {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        [UsedImplicitly]
        public void Show(bool isWin) {
            GameManager.GameState = GameState.Menu;

            TitleText.text = isWin ? "Congratulations, you won!" : "GAME OVER";
            ScoreText.text = PlayerStats.I.Score.ToString().ScoreFormat().Monospaced();

            // Fade in menu
            FadeManager.I.Fade(0f, Constants.PAUSE_TRANSITION_TIME, setAlpha, show, FadeCurve);
        }

        [UsedImplicitly]
        public void TryAgain() {
            GameManager.GameState = GameState.Running;
            GameManager.IsGameOver = false;
            Time.timeScale = State.I.PlayConfiguration.GameSpeed;

            SceneManager.I.RestartScene();
        }

        [UsedImplicitly]
        public void GoToMainMenu() {
            GameManager.IsGameOver = false;
            Time.timeScale = 1f;

            SceneManager.I.GoTo(Constants.MAIN_MENU, false);
        }

        [UsedImplicitly]
        public void Exit() {
            SceneManager.I.ExitGame();
        }

        private void show() {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
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
