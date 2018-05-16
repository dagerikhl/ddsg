using UnityEngine;

namespace DdSG {

    public class Clickable<T>: MonoBehaviour {

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        protected T clickable { get { return GetComponent<T>(); } }

        protected void PerformClickBehaviour() {
            SoundsManager.I.PlayClickSound();
        }

        protected void PerformMildClickBehaviour() {
            SoundsManager.I.PlayMildClickSound();
        }

    }

}
