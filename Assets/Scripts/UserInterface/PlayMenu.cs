using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class PlayMenu: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public SceneManager SceneManager;

        public Dropdown DifficultyDropdown;
        public Dropdown GameSpeedDropdown;
        public Toggle OwaspFilterToggle;
        public InputField EntitiesInputField;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members

        private void Start() {
            DifficultyDropdown.AddOptions(Constants.DIFFICULTIES.ToList());
            GameSpeedDropdown.AddOptions(Constants.GAME_SPEEDS.ToList());
            // TODO Add the rest
        }

        [UsedImplicitly]
        public void StartGame() {
            State.PlayConfiguration = new PlayConfiguration {
                Difficulty = float.Parse(DifficultyDropdown.captionText.text.Replace(" %", ""))/100f,
                GameSpeed = float.Parse(GameSpeedDropdown.captionText.text.Replace("x", "")),
                OwaspFilter = OwaspFilterToggle.isOn,
                Entities = EntitiesInputField.text.Split(',')
            };

            SceneManager.GoTo(Constants.GAME_VIEW);
        }

    }

}
