using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class BuildManager: SingletonBehaviour<BuildManager> {

        protected BuildManager() {
        }

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public GameObject BuildEffect;
        public GameObject SellEffect;

        public GameObject CourseOfActionPrefab;

        public ScrollRect CourseOfActionScrollRect;
        public Transform CourseOfActionButtonsContainer;
        public CourseOfActionButton CourseOfActionButtonPrefab;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]
        private CourseOfAction currentCourseOfAction;

        // Private and protected members

        public void CreateCourseOfActionButtons() {
            foreach (var courseOfAction in State.I.GameEntities.SDOs.course_of_actions) {
                var button = Instantiate(CourseOfActionButtonPrefab, CourseOfActionButtonsContainer);
                button.parentScrollRect = CourseOfActionScrollRect;
                button.Initialize(courseOfAction);
            }
        }

        public void ImplementMitigation(IPlacementArea area, IntVector2 gridPosition, IntVector2 sizeOffset) {
            if (currentCourseOfAction != null) {
                area.Occupy(gridPosition, sizeOffset, PlacementTileState.Filled);
                var mitigation = UnityHelper
                                 .Instantiate(CourseOfActionPrefab, area.GridToWorld(gridPosition, sizeOffset))
                                 .GetComponent<MitigationBehaviour>();
                mitigation.Initialize(currentCourseOfAction);

                ExitBuildMode();
            }
        }

        public void EnterBuildMode(CourseOfAction courseOfAction) {
            GameManager.IsBuilding = false;
            currentCourseOfAction = courseOfAction;
        }

        public void ExitBuildMode() {
            GameManager.IsBuilding = false;
            currentCourseOfAction = null;
        }

    }

}
