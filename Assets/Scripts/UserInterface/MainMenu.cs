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

        public void GoToLevelSelectMenu() {
            SceneManager.GoTo(Constants.LEVEL_SELECT_MENU);
        }

        public void GoToOptionsMenu() {
            SceneManager.GoTo(Constants.OPTIONS_MENU);
        }

        public void GoToHighscoreMenu() {
            SceneManager.GoTo(Constants.HIGHSCORE_MENU);
        }

        public void GoToAboutMenu() {
            SceneManager.GoTo(Constants.ABOUT_MENU);
        }

        public void ExitGame() {
            Debug.Log("Exiting game...");
            Application.Quit();
        }

    }

}
