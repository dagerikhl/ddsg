using System.Linq;

namespace DdSG {

    public class State: SingletonBehaviour<State> {

        protected State() {
            AmbientEnabled = true;
            AmbientVolume = 1f;
            SoundsEnabled = true;
            SoundsVolume = 1f;
            PlayConfiguration = new PlayConfiguration();
        }

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
        public bool AmbientEnabled { get; set; }
        public float AmbientVolume { get; set; }
        public bool SoundsEnabled { get; set; }
        public float SoundsVolume { get; set; }

        // Play configuration

        public PlayConfiguration PlayConfiguration { get; set; }

        // Entities
        public Entities Entities { get; set; }

    }

}
