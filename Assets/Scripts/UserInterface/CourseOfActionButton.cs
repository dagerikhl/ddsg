using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DdSG {

    public class CourseOfActionButton: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public Image Icon;
        public TextMeshProUGUI Label;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        [HideInInspector]
        public HoverBehaviour HoverBehaviour;
        [HideInInspector]
        public ActionEvents ActionEvents;

        public ScrollRect parentScrollRect { private get; set; }

        // Private and protected members

        private void Awake() {
            HoverBehaviour = GetComponent<HoverBehaviour>();
            ActionEvents = GetComponent<ActionEvents>();
        }

        public void OnBeginDrag(PointerEventData eventData) {
            parentScrollRect.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData) {
            parentScrollRect.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData) {
            parentScrollRect.OnEndDrag(eventData);
        }

        public void OnScroll(PointerEventData data) {
            parentScrollRect.OnScroll(data);
        }

        public void Initialize(CourseOfAction courseOfAction) {
            Label.text = courseOfAction.custom.category;

            HoverBehaviour.Title = courseOfAction.custom.category;
            HoverBehaviour.Text = Formatter.BuildStixDataEntityDescription(courseOfAction);

            HoverBehaviour.ActionText = "implement";
            ActionEvents.PrimaryAction = () => {
                // TODO Start build process
                Logger.Debug("Building");
            };
            HoverBehaviour.HasSecondaryAction = ReferencesHelper.AddReferencesAsAction(courseOfAction, ActionEvents);
        }

    }

}
