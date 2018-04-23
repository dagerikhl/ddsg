using UnityEngine.UI;

namespace DdSG {

    public class SliderBehaviour: ClickBehaviour<Slider> {

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members

        private void Start() {
            clickable.onValueChanged.AddListener((value) => PerformMildClickBehaviour());
        }

    }

}
