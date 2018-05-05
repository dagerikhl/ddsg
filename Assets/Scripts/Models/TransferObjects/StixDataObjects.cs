using System;

// ReSharper disable InconsistentNaming

namespace DdSG {

    [Serializable]
    public class StixDataObjects: TransferObjectBase {

        public AttackPattern[] attack_patterns;
        public CourseOfAction[] course_of_actions;
        public Asset[] assets;

    }

}
