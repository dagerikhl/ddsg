using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class ButtonBehaviour: MonoBehaviour {

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members
        private Button button { get { return GetComponent<Button>(); } }

        private void Start() {
            button.onClick.AddListener(SoundsManager.I.PlayClickSound);
        }

    }

}
