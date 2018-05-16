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
            HelperObjects.CameraManager.UiIsBlocking = true;
        }

        public void OnPointerExit(PointerEventData eventData) {
            HelperObjects.CameraManager.UiIsBlocking = false;
        }

    }

}
