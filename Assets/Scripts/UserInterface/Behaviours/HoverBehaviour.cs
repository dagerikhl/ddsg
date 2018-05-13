using UnityEngine;
using UnityEngine.EventSystems;

namespace DdSG {

    public class HoverBehaviour: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public bool GameOverlay;
        public string Title;
        [TextArea(20, 50)]
        public string Text;
        public bool ShowUnder;
        public bool ShowOnLeft;

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

            var position = GameOverlay ? Input.mousePosition : transform.position;
            HelperObjects.HoverOverlay.SetPosition(position, ShowUnder, ShowOnLeft);

            HelperObjects.HoverOverlay.SetActive(true);
        }

        private void hideHoverOverlay() {
            HelperObjects.HoverOverlay.SetActive(false);
        }

        private void addPointerEntersEvent() {
            EventTrigger.Entry eventType = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };

            eventType.callback.AddListener(
                (eventData) => {
                    // Logger.Debug("Showing overlay.");
                    showHoverOverlay();
                });

            eventTrigger.triggers.Add(eventType);
        }

        private void addPointerExitsEvent() {
            EventTrigger.Entry eventType = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };

            eventType.callback.AddListener(
                (eventData) => {
                    // Logger.Debug("Hiding overlay.");
                    hideHoverOverlay();
                });

            eventTrigger.triggers.Add(eventType);
        }

    }

}
