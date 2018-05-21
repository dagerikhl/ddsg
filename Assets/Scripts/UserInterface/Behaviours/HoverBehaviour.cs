using UnityEngine;
using UnityEngine.EventSystems;

namespace DdSG {

    public class HoverBehaviour: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

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
        public bool HasSecondaryAction { private get; set; }

        // Private and protected members
        private HoverOverlay hoverOverlay;

        private bool HasPrimaryAction { get { return !string.IsNullOrEmpty(ActionText); } }
        private bool HasActions { get { return HasPrimaryAction || HasSecondaryAction; } }

        public void OnPointerEnter(PointerEventData eventData) {
            destroyOldHoverOverlays();
            createHoverOverlay();

            if (HasActions) {
                CursorManager.I.SetTemporaryCursor(CursorType.Pointer);
            }
        }

        public void OnPointerExit(PointerEventData eventData) {
            hideHoverOverlay();

            CursorManager.I.ResetTemporaryCursor();
        }

        private void destroyOldHoverOverlays() {
            foreach (var hoverOverlayGo in GameObject.FindGameObjectsWithTag(Constants.HOVER_OVERLAY_TAG)) {
                var hoverOverlay = hoverOverlayGo.GetComponent<HoverOverlay>();
                hoverOverlay.Destroy();
            }
        }

        private void createHoverOverlay() {
            hoverOverlay = UnityHelper.Instantiate(HelperObjects.HoverOverlayPrefab).GetComponent<HoverOverlay>();

            var position = GameOverlay ? Input.mousePosition : transform.position;
            var direction = new Vector3(ShowOnLeft ? -1 : 1, ShowUnder ? -1 : 1);
            var data = new DescriptionData {
                Title = Title,
                Text = Text,
                ActionText = Formatter.BuildActionText(ActionText)
            };
            hoverOverlay.Initialize(position, direction, data);
        }

        private void hideHoverOverlay() {
            hoverOverlay.Destroy();
        }

        private void OnDestroy() {
            if (hoverOverlay != null) {
                hoverOverlay.Destroy();
            }
        }

    }

}
