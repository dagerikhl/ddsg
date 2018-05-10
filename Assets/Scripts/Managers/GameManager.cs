using TMPro;
using UnityEngine;

namespace DdSG {

    public class GameManager: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public TextMeshProUGUI GameTime;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector

        // Private members
        private float secondsElapsed;

        private void Update() {
            secondsElapsed += Time.deltaTime;

            GameTime.text = TimeHelper.FormatTime(secondsElapsed);
        }

    }

}
