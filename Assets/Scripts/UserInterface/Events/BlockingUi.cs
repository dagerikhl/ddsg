using UnityEngine;
using UnityEngine.EventSystems;

namespace DdSG {

    public class BlockingUi: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        public void OnPointerEnter(PointerEventData eventData) {
            GameManager.IsUiBlocking = true;
        }

        public void OnPointerExit(PointerEventData eventData) {
            GameManager.IsUiBlocking = false;
        }

    }

}
