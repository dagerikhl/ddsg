using System.Linq;

namespace DdSG {

    public static class State {

        // Scenes
        private static string lastScene = Constants.MAIN_MENU;
        private static string currentScene = Constants.MAIN_MENU;

        public static string LastScene {
            get { return lastScene; }
            set {
                if (Constants.SCENES.Contains(value)) { lastScene = value; }
            }
        }

        public static string CurrentScene {
            get { return currentScene; }
            set {
                if (Constants.SCENES.Contains(value)) {
                    currentScene = value;
                }
            }
        }

        // Level
        private static int level = 1;
        public static int Level { get { return level; } set { level = value; } }

    }

}
