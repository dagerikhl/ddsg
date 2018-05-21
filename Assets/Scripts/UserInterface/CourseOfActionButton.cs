using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DdSG {

    public class CourseOfActionButton: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public HoverBehaviour HoverBehaviour;

        // TODO This is added for later use
        public Image Icon;
        public TextMeshProUGUI Label;
        public TextMeshProUGUI Cost;

        public GameObject Blocker;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        public ScrollRect parentScrollRect { private get; set; }

        // Private and protected members
        private int value;
        private int Value {
            get { return value; }
            set {
                this.value = value;
                Cost.text = value.ToString();
            }
        }

        private void Update() {
            Blocker.SetActive(PlayerStats.I.Worth < Value);
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
            Value = courseOfAction.GetValue();

            Label.text = courseOfAction.custom.category;

            HoverBehaviour.Title = courseOfAction.custom.category;
            HoverBehaviour.Text = Formatter.BuildStixDataEntityDescription(courseOfAction);

            HoverBehaviour.ActionText = "implement";
            HoverBehaviour.PrimaryAction = () => {
                // Destroy the old ghost if it's already in the game
                var oldGhost = FindObjectOfType<GhostMitigationBehaviour>();
                if (oldGhost != null) {
                    oldGhost.DestroyGhost();
                }

                BuildManager.I.EnterBuildMode(courseOfAction);
                UnityHelper.Instantiate(HelperObjects.GhostMitigationPrefab);
            };
            HoverBehaviour.HasSecondaryAction = ReferencesHelper.AddReferencesAsAction(courseOfAction, HoverBehaviour);
        }

    }

}
