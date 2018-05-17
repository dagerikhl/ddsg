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
                Logger.Debug("Building");
                // TODO Start build process
                /**
                 * TODO
                 * 1. Create ghost tower that follows mouse position raycasted to world with flattenede height.
                 * 2. Listen for click.
                 * 3. Destroy ghost on click.
                 * 4. Instantiate tower on click.
                 */
                BuildManager.I.CurrentCourseOfAction = courseOfAction;
                var ghost = UnityHelper.Instantiate(HelperObjects.GhostMitigationPrefab)
                                       .GetComponent<GhostMitigationBehaviour>();
            };
            HoverBehaviour.HasSecondaryAction = ReferencesHelper.AddReferencesAsAction(courseOfAction, ActionEvents);
        }

    }

}
