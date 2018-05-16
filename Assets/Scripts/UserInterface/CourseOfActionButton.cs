using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DdSG {

    public class CourseOfActionButton: MonoBehaviour, IPointerClickHandler {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public Image Icon;
        public TextMeshProUGUI Label;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        [HideInInspector]
        public HoverBehaviour HoverBehaviour;

        private void Awake() {
            HoverBehaviour = GetComponent<HoverBehaviour>();
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                // TODO Start build process
                Logger.Debug("Building");
            } else if (eventData.button == PointerEventData.InputButton.Right) {
                // TODO Open source from external refs
                Logger.Debug("Opening source");
                // Application.OpenURL(url);
            }
        }

    }

}
