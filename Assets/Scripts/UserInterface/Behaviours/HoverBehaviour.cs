using UnityEngine;
using UnityEngine.EventSystems;

namespace DdSG {

    public class HoverBehaviour: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public string Title;
        [TextArea(20, 50)]
        public string Text;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private EventTrigger eventTrigger;

        private void Awake() {
            eventTrigger = gameObject.AddComponent<EventTrigger>();

            addPointerEntersEvent();
            addPointerExitsEvent();
        }

        private void showHoverOverlay() {
            HelperObjects.HoverOverlay.SetTitle(Title);
            HelperObjects.HoverOverlay.SetText(Text);

            HelperObjects.HoverOverlay.SetPosition(transform.position);

            HelperObjects.HoverOverlay.SetActive(true);
        }

        private void hideHoverOverlay() {
            HelperObjects.HoverOverlay.SetActive(false);
        }

        private void addPointerEntersEvent() {
            EventTrigger.Entry eventType = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };

            eventType.callback.AddListener((eventData) => { showHoverOverlay(); });

            eventTrigger.triggers.Add(eventType);
        }

        private void addPointerExitsEvent() {
            EventTrigger.Entry eventType = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };

            eventType.callback.AddListener((eventData) => { hideHoverOverlay(); });

            eventTrigger.triggers.Add(eventType);
        }

    }

}
