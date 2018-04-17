using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class LevelSelectMenu: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public SceneManager SceneManager;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members

        public void StartLevel(Button levelButton) {
            State.Level = levelButton.GetComponent<SelectLevelButton>().LevelNumber;
            SceneManager.GoTo(Constants.GAME_VIEW);
        }

    }

}
