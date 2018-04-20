using JetBrains.Annotations;
using UnityEngine;

namespace DdSG {

    public class MainMenu: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public SceneManager SceneManager;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members

        [UsedImplicitly]
        public void GoToLevelSelectMenu() {
            SceneManager.GoTo(Constants.PLAY_MENU);
        }

        [UsedImplicitly]
        public void GoToOptionsMenu() {
            SceneManager.GoTo(Constants.OPTIONS_MENU);
        }

        [UsedImplicitly]
        public void GoToHighscoreMenu() {
            SceneManager.GoTo(Constants.HIGHSCORE_MENU);
        }

        [UsedImplicitly]
        public void GoToAboutMenu() {
            SceneManager.GoTo(Constants.ABOUT_MENU);
        }

        [UsedImplicitly]
        public void ExitGame() {
            Debug.Log("Exiting game...");
            Application.Quit();
        }

    }

}
