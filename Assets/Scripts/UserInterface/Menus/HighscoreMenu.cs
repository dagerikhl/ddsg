using System.Linq;
using UnityEngine;

namespace DdSG {

    public class HighscoreMenu: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public Transform HighscoresContainer;
        public GameObject HighscorePrefab;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        private void Awake() {
            // Abort if no highscores exists
            if (!FileClient.I.FileExists(Constants.HIGHSCORES_FILENAME)) {
                return;
            }

            // Fetch highscores from file
            var highscores = FileClient.I.LoadFromFile<Highscore[]>(Constants.HIGHSCORES_FILENAME);

            // Initialize highscores with data
            highscores = highscores.OrderByDescending((h) => h.Score).ToArray();
            for (var i = 0; i < highscores.Length; i++) {
                var highscoreBehaviour =
                    Instantiate(HighscorePrefab, HighscoresContainer).GetComponent<HighscoreBehaviour>();
                highscoreBehaviour.Initialize(i + 1, highscores[i]);
            }
        }

    }

}
