using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class PlayMenu: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public Dropdown DifficultyDropdown;
        public Dropdown GameSpeedDropdown;
        public Toggle OwaspFilterToggle;
        public InputField EntitiesInputField;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        private void Start() {
            // Disable sounds when setting the initial state
            SoundsManager.I.DisableAudio();

            DifficultyDropdown.AddOptions(Constants.DIFFICULTY_OPTIONS.ToList());
            GameSpeedDropdown.AddOptions(Constants.GAME_SPEED_OPTIONS.ToList());

            DifficultyDropdown.value = Constants.DIFFICULTY_OPTIONS.ToList()
                                                .IndexOf(unparseDifficulty(State.I.PlayConfiguration.Difficulty));
            GameSpeedDropdown.value = Constants.GAME_SPEED_OPTIONS.ToList()
                                               .IndexOf(unparseGameSpeed(State.I.PlayConfiguration.GameSpeed));
            OwaspFilterToggle.isOn = State.I.PlayConfiguration.OwaspFilter;
            EntitiesInputField.text = string.Join(",", State.I.PlayConfiguration.Entities);

            // Restore sounds after setting the initial state
            SoundsManager.I.EnableAudio();
        }

        [UsedImplicitly]
        public void StartGame() {
            State.I.PlayConfiguration = new PlayConfiguration {
                Difficulty = parseDifficulty(DifficultyDropdown.captionText.text),
                GameSpeed = parseGameSpeed(GameSpeedDropdown.captionText.text),
                OwaspFilter = OwaspFilterToggle.isOn,
                Entities = EntitiesInputField.text.Split(',')
            };

            GameManager.GameState = GameState.Running;
            SceneManager.I.GoTo(Constants.GAME_VIEW, false);
        }

        private float parseDifficulty(string difficulty) {
            return float.Parse(difficulty.Replace(" %", ""))/100f;
        }

        private string unparseDifficulty(float difficulty) {
            return difficulty*100f + " %";
        }

        private float parseGameSpeed(string gameSpeed) {
            return float.Parse(gameSpeed.Replace("x", ""));
        }

        private string unparseGameSpeed(float gameSpeed) {
            return Mathf.RoundToInt(gameSpeed) + "x";
        }

    }

}
