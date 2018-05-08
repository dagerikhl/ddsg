using System.Linq;

namespace DdSG {

    public class State: PersistentSingletonBehaviour<State> {

        protected State() {
        }

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        protected override string persistentTag { get { return "State"; } }

        // Scenes
        private string lastScene = Constants.MAIN_MENU;

        public string LastScene {
            get { return lastScene; }
            set {
                if (Constants.SCENES.Contains(value)) { lastScene = value; }
            }
        }

        private string currentScene = Constants.MAIN_MENU;
        public string CurrentScene {
            get { return currentScene; }
            set {
                if (Constants.SCENES.Contains(value)) { currentScene = value; }
            }
        }

        // Audio
        // TODO Impl. fetching of these settings from file
        public Options Options { get; set; }

        // Play configuration
        public PlayConfiguration PlayConfiguration { get; set; }

        // Entities
        public Entities Entities { get; set; }

        private void Awake() {
            Options = new Options {
                AmbientEnabled = true,
                AmbientVolume = 1f,
                SoundsEnabled = true,
                SoundsVolume = 1f
            };

            // Default play configuration
            PlayConfiguration = new PlayConfiguration {
                Difficulty = 0.5f,
                GameSpeed = 1f,
                OwaspFilter = true,
                Entities = new string[] { "attack_patterns", "weaknesses", "assets", "course_of_ations" }
            };
        }

    }

}
