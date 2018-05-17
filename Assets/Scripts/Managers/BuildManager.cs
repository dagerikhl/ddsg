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

        public ScrollRect CourseOfActionScrollRect;
        public Transform CourseOfActionButtonsContainer;
        public CourseOfActionButton CourseOfActionButtonPrefab;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]
        public CourseOfAction CurrentCourseOfAction { private get; set; }

        // Private and protected members

        public void CreateCourseOfActionButtons() {
            foreach (var courseOfAction in State.I.GameEntities.SDOs.course_of_actions) {
                var button = Instantiate(CourseOfActionButtonPrefab, CourseOfActionButtonsContainer);
                button.parentScrollRect = CourseOfActionScrollRect;
                button.Initialize(courseOfAction);
            }
        }

        public void ImplementMitigation(IPlacementArea area, IntVector2 gridPosition, IntVector2 sizeOffset) {
            Logger.Debug("Trying to palce ghost");
            area.Occupy(gridPosition, sizeOffset, PlacementTileState.Filled);
            // TODO Place currentCourseOfAction here

            CurrentCourseOfAction = null;
        }

    }

}
