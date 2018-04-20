using System.Linq;

namespace DdSG {

    public static class State {

        // Scenes
        private static string lastScene = Constants.MAIN_MENU;

        public static string LastScene {
            get { return lastScene; }
            set {
                if (Constants.SCENES.Contains(value)) { lastScene = value; }
            }
        }

        private static string currentScene = Constants.MAIN_MENU;

        public static string CurrentScene {
            get { return currentScene; }
            set {
                if (Constants.SCENES.Contains(value)) { currentScene = value; }
            }
        }

        // Play configuration
        private static PlayConfiguration playConfiguration = new PlayConfiguration();

        public static PlayConfiguration PlayConfiguration {
            get { return playConfiguration; }
            set { playConfiguration = value; }
        }

    }

}
