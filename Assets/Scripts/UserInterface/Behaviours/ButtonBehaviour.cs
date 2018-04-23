using UnityEngine.UI;

namespace DdSG {

    public class ButtonBehaviour: ClickBehaviour<Button> {

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        private void Start() {
            clickable.onClick.AddListener(PerformClickBehaviour);
        }

    }

}
