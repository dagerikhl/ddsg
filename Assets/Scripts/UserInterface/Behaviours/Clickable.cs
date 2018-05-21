using UnityEngine;
using UnityEngine.EventSystems;

namespace DdSG {

    public class Clickable<T>: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        protected T clickable { get { return GetComponent<T>(); } }

        public void OnPointerEnter(PointerEventData eventData) {
            CursorManager.I.SetTemporaryCursor(CursorType.Pointer);
        }

        public void OnPointerExit(PointerEventData eventData) {
            CursorManager.I.ResetTemporaryCursor();
        }

        protected void PerformClickBehaviour() {
            SoundsManager.I.PlayClickSound();
        }

        protected void PerformMildClickBehaviour() {
            SoundsManager.I.PlayMildClickSound();
        }

    }

}
