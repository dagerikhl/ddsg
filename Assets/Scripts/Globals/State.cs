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

        // Play configuration
        private PlayConfiguration playConfiguration = new PlayConfiguration();

        public PlayConfiguration PlayConfiguration {
            get { return playConfiguration; }
            set { playConfiguration = value; }
        }

    }

}
