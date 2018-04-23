namespace DdSG {

    public class PlayConfiguration {

        public float Difficulty { get; set; }
        public float GameSpeed { get; set; }
        public bool OwaspFilter { get; set; }
        public string[] Entities { get; set; }

        public PlayConfiguration() {
            Difficulty = 0.5f;
            GameSpeed = 1f;
            OwaspFilter = true;
            Entities = new string[] { "attackPatterns", "weaknesses", "assets", "courseOfActions" };
        }

    }

}
