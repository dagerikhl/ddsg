using UnityEngine;

namespace DdSG {

    public class BuildManager: SingletonBehaviour<BuildManager> {

        protected BuildManager() {
        }

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public GameObject BuildEffect;
        public GameObject SellEffect;

        public Transform CourseOfActionButtonsContainer;
        public CourseOfActionButton CourseOfActionButtonPrefab;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        public void CreateCourseOfActionButtons() {
            foreach (var courseOfAction in State.I.GameEntities.SDOs.course_of_actions) {
                var button = Instantiate(CourseOfActionButtonPrefab, CourseOfActionButtonsContainer);
                button.Label.text = courseOfAction.custom.category;
                button.HoverBehaviour.Title = courseOfAction.custom.category;
                button.HoverBehaviour.Text = courseOfAction.FullDescription;
                button.HoverBehaviour.ActionText = "implement";
            }
        }

    }

}
