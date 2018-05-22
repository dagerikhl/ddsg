using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class HighscoreBehaviour: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public Text Place;
        public Text Time;
        public Text State;
        public Text Score;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        public void Initialize(int place, Highscore highscore) {
            Place.text = place + ".";
            Time.text = highscore.Time.ToString("yyyy-MM-dd HH:mm");
            State.text = highscore.State == GameOverState.Win ? "Win!" : "Loss.";
            Score.text = string.Format("{0:###,##0}", highscore.Score);
        }

    }

}
