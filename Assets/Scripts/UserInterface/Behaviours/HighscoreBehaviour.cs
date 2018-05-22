using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class HighscoreBehaviour: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public Text Place;
        public Text State;
        public Text Score;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        public void Initialize(int place, Highscore highscore) {
            Place.text = place + ".";
            State.text = "The game ended with a: " + (highscore.State == GameOverState.Win ? "Win!" : "Loss.");
            Score.text = string.Format("{0:###,##0}", highscore.Score);
        }

    }

}
