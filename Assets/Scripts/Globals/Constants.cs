namespace DdSG {

    public static class Constants {

        // Scenes
        public const string LOADING_SCREEN = "LoadingScreen";
        public const string MAIN_MENU = "MainMenu";
        public const string OPTIONS_MENU = "OptionsMenu";
        public const string HIGHSCORE_MENU = "HighscoreMenu";
        public const string ABOUT_MENU = "AboutMenu";
        public const string PLAY_MENU = "PlayMenu";
        public const string GAME_VIEW = "GameView";
        public static readonly string[] SCENES = {
            LOADING_SCREEN,
            MAIN_MENU,
            OPTIONS_MENU,
            HIGHSCORE_MENU,
            ABOUT_MENU,
            PLAY_MENU,
            GAME_VIEW
        };

        // Scene transitions
        public const float SCENE_TRANSITION_TIME = 1.2f;
        public const float PAUSE_TRANSITION_TIME = 0.2f;
        public const float HOVER_TRANSITION_TIME = 0.1f;

        // Layers
        public const int PATH_LAYER = 8;

        // Play configuration
        public static readonly string[] DIFFICULTY_OPTIONS = { "10 %", "25 %", "50 %", "75 %", "90 %", "100 %" };
        public static readonly string[] GAME_SPEED_OPTIONS = { "1x", "2x", "3x" };

        // URLs
        public const string API_URL = "https://ddsg-server.herokuapp.com";
        public const string API_URL_DEVELOPMENT = "http://localhost:8000";

        // Files
        public const string FILE_DATA_EXT = ".ddsgd";
        public const string OPTIONS_FILENAME = "options";
        public const string PLAY_CONFIGURATION_FILENAME = "playConfiguration";
        public const string ENTITIES_FILENAME = "entities";
        public const string ENTITIES_BACKUP_FILENAME = "entities-backup";
        public const string HIGHSCORES_FILENAME = "highscores";

        // Game constants
        public const float SELL_PERCENTAGE = 0.75f;

        // Tags
        public const string ATTACK_TAG = "Attack";
        public const string MITIGATION_TAG = "Mitigation";
        public const string HOVER_OVERLAY_TAG = "HoverOverlay";

    }

}
