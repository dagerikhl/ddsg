using JetBrains.Annotations;
using UnityEngine;

namespace DdSG {

    public class MainMenu: MonoBehaviour {

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members

        [UsedImplicitly]
        public void GoToLevelSelectMenu() {
            SceneManager.I.GoTo(Constants.PLAY_MENU);
        }

        [UsedImplicitly]
        public void GoToOptionsMenu() {
            SceneManager.I.GoTo(Constants.OPTIONS_MENU);
        }

        [UsedImplicitly]
        public void GoToHighscoreMenu() {
            SceneManager.I.GoTo(Constants.HIGHSCORE_MENU);
        }

        [UsedImplicitly]
        public void GoToAboutMenu() {
            SceneManager.I.GoTo(Constants.ABOUT_MENU);
        }

        [UsedImplicitly]
        public void ExitGame() {
            Debug.Log("Exiting game...");
            Application.Quit();
        }

    }

}
