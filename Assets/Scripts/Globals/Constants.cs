namespace DdSG {

    public static class Constants {

        // Scenes
        public const string MAIN_MENU = "MainMenu";
        public const string OPTIONS_MENU = "OptionsMenu";
        public const string HIGHSCORE_MENU = "HighscoreMenu";
        public const string ABOUT_MENU = "AboutMenu";
        public const string PLAY_MENU = "PlayMenu";
        public const string GAME_VIEW = "GameView";
        public static readonly string[] SCENES = {
            MAIN_MENU,
            OPTIONS_MENU,
            HIGHSCORE_MENU,
            ABOUT_MENU,
            PLAY_MENU,
            GAME_VIEW
        };

        // Scene transitions
        public const float SCENE_TRANSITION_TIME = 1.2f;

        // Play configuration
        public static readonly string[] DIFFICULTY_OPTIONS = { "10 %", "25 %", "50 %", "75 %", "90 %", "100 %" };
        public static readonly string[] GAME_SPEED_OPTIONS = { "1x", "2x", "3x" };

    }

}
