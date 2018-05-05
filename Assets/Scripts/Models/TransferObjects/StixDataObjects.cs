using System;
using System.Diagnostics.CodeAnalysis;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class StixDataObjects: TransferObjectBase {

        public AttackPattern[] attack_patterns;
        public CourseOfAction[] course_of_actions;
        public Asset[] assets;

    }

}
