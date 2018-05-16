using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class CourseOfActionButton: MonoBehaviour {

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
        [HideInInspector]
        public ActionEvents ActionEvents;

        private void Awake() {
            HoverBehaviour = GetComponent<HoverBehaviour>();
            ActionEvents = GetComponent<ActionEvents>();
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
