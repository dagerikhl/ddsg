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
            SceneManager.Instance.GoTo(Constants.PLAY_MENU);
        }

        [UsedImplicitly]
        public void GoToOptionsMenu() {
            SceneManager.Instance.GoTo(Constants.OPTIONS_MENU);
        }

        [UsedImplicitly]
        public void GoToHighscoreMenu() {
            SceneManager.Instance.GoTo(Constants.HIGHSCORE_MENU);
        }

        [UsedImplicitly]
        public void GoToAboutMenu() {
            SceneManager.Instance.GoTo(Constants.ABOUT_MENU);
        }

        [UsedImplicitly]
        public void ExitGame() {
            Debug.Log("Exiting game...");
            Application.Quit();
        }

    }

}
