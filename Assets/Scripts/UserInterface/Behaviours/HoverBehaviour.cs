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
        public string ActionText;
        public bool ShowUnder;
        public bool ShowOnLeft;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private EventTrigger eventTrigger;
        private HoverOverlay hoverOverlay;

        private void Awake() {
            eventTrigger = gameObject.AddComponent<EventTrigger>();

            addPointerEntersEvent();
            addPointerExitsEvent();
        }

        private void createHoverOverlay() {
            hoverOverlay = Instantiate(HelperObjects.HoverOverlayPrefab, HelperObjects.Ephemerals)
                .GetComponent<HoverOverlay>();

            var position = GameOverlay ? Input.mousePosition : transform.position;
            var direction = new Vector3(ShowOnLeft ? -1 : 1, ShowUnder ? -1 : 1);
            var data = new DescriptionData { Title = Title, Text = Text, ActionText = ActionText };
            hoverOverlay.Initialize(position, direction, data);
        }

        private void hideHoverOverlay() {
            hoverOverlay.Destroy();
        }

        private void addPointerEntersEvent() {
            EventTrigger.Entry eventType = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            eventType.callback.AddListener(
                (eventData) => {
                    // Logger.Debug("Showing overlay.");
                    createHoverOverlay();
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
