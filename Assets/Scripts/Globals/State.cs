using System.Linq;

namespace DdSG {

    public class State: Singleton<State> {

        protected State() {
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
        private bool ambientEnabled = true;
        public bool AmbientEnabled { get { return ambientEnabled; } set { ambientEnabled = value; } }

        private float ambientVolume = 1f;
        public float AmbientVolume { get { return ambientVolume; } set { ambientVolume = value; } }

        private bool soundsEnabled = true;
        public bool SoundsEnabled { get { return soundsEnabled; } set { soundsEnabled = value; } }

        private float soundsVolume = 1f;
        public float SoundsVolume { get { return soundsVolume; } set { soundsVolume = value; } }

        // Play configuration
        private PlayConfiguration playConfiguration = new PlayConfiguration();

        public PlayConfiguration PlayConfiguration {
            get { return playConfiguration; }
            set { playConfiguration = value; }
        }

    }

}
