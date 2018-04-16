using System.Linq;

namespace DdSG {

    public static class State {

        private static string lastScene = Constants.MAIN_MENU;

        public static string LastScene {
            get { return lastScene; }
            set {
                if (Constants.SCENES.Contains(value)) {
                    lastScene = value;
                }
            }
        }

        private static string currentScene = Constants.MAIN_MENU;

        public static string CurrentScene {
            get { return currentScene; }
            set {
                if (Constants.SCENES.Contains(value)) {
                    currentScene = value;
                }
            }
        }

    }

}
