using UnityEngine;

namespace DdSG {

    public static class CourseOfActionExtensions {

        // TODO This is fairly random, but made so because of missing data in CourseOfAction and to be consistent
        public static int GetValue(this CourseOfAction courseOfAction) {
            var categoryValue = courseOfAction.custom.category == null ? 10 : courseOfAction.custom.category.Length;
            var mitigationValue =
                courseOfAction.custom.mitigation == null ? 15 : courseOfAction.custom.mitigation.Length;
            var referencesValue = courseOfAction.external_references == null ? 20
                : courseOfAction.external_references.Length;

            return Mathf.RoundToInt((categoryValue + mitigationValue + referencesValue)/2f/10f)*5;
        }

        // TODO This is fairly random, but made so because of missing data in CourseOfAction and to be consistent
        public static DamageAttribute GetDamage(this CourseOfAction courseOfAction) {
            var categoryValue = courseOfAction.custom.category == null ? 8f : courseOfAction.custom.category.Length;
            var mitigationValue =
                courseOfAction.custom.mitigation == null ? 5f : courseOfAction.custom.mitigation.Length;
            var referencesValue = courseOfAction.external_references == null ? 25f
                : courseOfAction.external_references.Length;

            var baseDamage = (categoryValue*2 + mitigationValue + referencesValue/2f)/5f;
            return new DamageAttribute { MinimumDamage = (baseDamage - 2)*0.8f, MaximumDamage = (baseDamage + 4)*1.1f };
        }

        // TODO This is fairly random, but made so because of missing data in CourseOfAction and to be consistent
        public static float GetRange(this CourseOfAction courseOfAction) {
            var categoryValue = courseOfAction.custom.category == null ? 5f : courseOfAction.custom.category.Length;
            var mitigationValue =
                courseOfAction.custom.mitigation == null ? 19f : courseOfAction.custom.mitigation.Length;
            var referencesValue = courseOfAction.external_references == null ? 30f
                : courseOfAction.external_references.Length;

            return 5 + (categoryValue + mitigationValue*2 + referencesValue)/8f;
        }

        // TODO This is fairly random, but made so because of missing data in CourseOfAction and to be consistent
        public static float GetFireRate(this CourseOfAction courseOfAction) {
            var categoryValue = courseOfAction.custom.category == null ? 10f : courseOfAction.custom.category.Length;
            var mitigationValue =
                courseOfAction.custom.mitigation == null ? 15f : courseOfAction.custom.mitigation.Length;
            var referencesValue = courseOfAction.external_references == null ? 20f
                : courseOfAction.external_references.Length;

            return Mathf.Min(Mathf.Max((categoryValue*2 - mitigationValue + referencesValue)/20f, 0f) + 1f, 4f);
        }

    }

}
