using UnityEngine;

namespace DdSG {

    public static class CourseOfActionExtensions {

        // TODO This is fairly random, but made so because of missing data in CourseOfAction and to be consistent1
        public static int GetValue(this CourseOfAction courseOfAction) {
            var categoryValue = courseOfAction.custom.category == null ? 10 : courseOfAction.custom.category.Length;
            var mitigationValue =
                courseOfAction.custom.mitigation == null ? 15 : courseOfAction.custom.mitigation.Length;
            var referencesValue = courseOfAction.external_references == null ? 20
                : courseOfAction.external_references.Length;

            return Mathf.RoundToInt((categoryValue + mitigationValue + referencesValue)/2f/10f)*5;
        }

    }

}
