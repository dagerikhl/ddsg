using System;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DdSG {

    // TODO This code has reach a critical point of spaghetti-mass and should be refactored severely.
    public static class Formatter {

        public static string TimeFormat(float seconds) {
            var secondsRounded = Mathf.RoundToInt(seconds);
            var time = new TimeSpan(0, 0, secondsRounded);

            return string.Format(
                "{0}{1:D2}:{2:D2}",
                time.Hours == 0 ? " " : "",
                time.Hours*60 + time.Minutes,
                time.Seconds);
        }

        public static string CounterFormat(float seconds) {
            return seconds < 0.01f ? "0.00".PadLeft(6).WithColor("#8c8c8c")
                : string.Format("{0:##0.00}", seconds).PadLeft(6);
        }

        public static string CountOfMaxFormat(int count, int max) {
            var countString = (count == 0 ? "-" : string.Format("{0:#0}", count)).PadLeft(2);
            var maxString = (max == 0 ? "-" : string.Format("{0:#0}", max)).PadLeft(2);
            return string.Format("{0}/{1}", countString, maxString);
        }

        public static string BuildStixDataEntityDescription(
            StixDataEntityBase entity,
            bool showDescription = true,
            bool showSources = true) {
            var sb = new StringBuilder();

            if (showDescription) {
                sb.AppendLine(entity.FullDescription);
                sb.AppendLine();
            }
            appendAttributes(sb, entity, showDescription);
            appendRelationships(sb, entity);
            if (showSources) {
                appendSources(sb, entity);
            }

            return sb.ToString();
        }

        public static string BuildActionText(string text) {
            return !string.IsNullOrEmpty(text) ? "Click to " + text : "";
        }

        private static void appendAttributes(StringBuilder sb, StixDataEntityBase entity, bool showDescription) {
            var hasAttributes = entity.GetType() == typeof(AttackPattern) || entity.GetType() == typeof(CourseOfAction);
            if (!hasAttributes) {
                return;
            }

            if (showDescription) {
                sb.AppendLine("<i><size=140%>Attributes</size></i>");
                sb.AppendLine();
            }

            if (entity.GetType() == typeof(AttackPattern)) {
                appendAttackPatternAttributes(sb, (AttackPattern) entity);
            } else if (entity.GetType() == typeof(CourseOfAction)) {
                appendCourseOfActionAttributes(sb, (CourseOfAction) entity);
            }
            sb.AppendLine("<indent=0>");
        }

        private static void appendAttackPatternAttributes(StringBuilder sb, AttackPattern attackPattern) {
            if (attackPattern.custom.severity != null) {
                sb.AppendLine(
                    buildAttributeText(
                        "Severity",
                        EnumHelper.GetEnumMemberAttributeValue((Scale) attackPattern.custom.severity)));
            }
            if (attackPattern.custom.likelihood != null) {
                sb.AppendLine(
                    buildAttributeText(
                        "Likelihood",
                        EnumHelper.GetEnumMemberAttributeValue((Scale) attackPattern.custom.likelihood)));
            }
            sb.AppendLine(
                buildAttributeText(
                    "Injection Vector",
                    buildInjectionVectorText(attackPattern.custom.injection_vector)));
            sb.AppendLine(buildAttributeText("Payload", attackPattern.custom.payload));
            sb.AppendLine(
                buildAttributeText("Activation Zone", buildActivationZoneText(attackPattern.custom.activation_zone)));
            sb.AppendLine(buildAttributeText("Impact", buildImpactText(attackPattern.custom.impact)));
        }

        private static void appendCourseOfActionAttributes(StringBuilder sb, CourseOfAction courseOfAction) {
            sb.AppendLine(
                buildAttributeText(
                    "Damage",
                    buildDamageText(courseOfAction.GetDamage(), courseOfAction.GetFireRate())));
            sb.AppendLine(buildAttributeText("Range", buildRangeText(courseOfAction.GetRange())));
            sb.AppendLine(buildAttributeText("Fire Rate", buildFireRateText(courseOfAction.GetFireRate())));
        }

        private static void appendRelationships(StringBuilder sb, StixDataEntityBase entity) {
            sb.AppendLine("<i><size=140%>Relationships</size></i>");
            sb.AppendLine();

            if (entity.GetType() == typeof(Asset)) {
                var asset = (Asset) entity;

                // Is targeted by attack patterns
                var targetedBy = State.I.GameEntities.SDOs.attack_patterns.Where(
                    (aP) => aP.custom.activation_zone.categories.Any(
                        (c) => c == asset.custom.category));
                var targetedByStrings = targetedBy.Select((aP) => aP.name);
                sb.AppendLine(buildAttributeText("Targeted by", targetedByStrings.Join("; ")));
            } else if (entity.GetType() == typeof(AttackPattern)) {
                var attackPattern = (AttackPattern) entity;

                // Targets assets
                var targets =
                    attackPattern.custom.activation_zone.categories.Select(EnumHelper.GetEnumMemberAttributeValue);
                sb.AppendLine(buildAttributeText("Targets", targets.Join("; ")));

                // Mitigated by course of actions
                var mitigatedBy =
                    State.I.GameEntities.SDOs.course_of_actions.Where((c) => c.RelatedAsSourceTo(attackPattern));
                var mitigatedByStrings = mitigatedBy.Select((c) => c.custom.mitigation);
                sb.AppendLine(buildAttributeText("Mitigated by", mitigatedByStrings.Join("; ")));
            } else if (entity.GetType() == typeof(CourseOfAction)) {
                var courseOfAction = (CourseOfAction) entity;

                // Mitigates attack patterns
                var mitigates =
                    State.I.GameEntities.SDOs.attack_patterns.Where((aP) => aP.RelatedAsTargetTo(courseOfAction));
                var mitigatesStrings = mitigates.Select((c) => c.name);
                sb.AppendLine(buildAttributeText("Mitigates", mitigatesStrings.Join("; ")));
            }
            sb.AppendLine("<indent=0>");
        }

        private static void appendSources(StringBuilder sb, StixDataEntityBase entity) {
            sb.Append("<i><size=140%>Sources</size></i>");
            if (entity.external_references.Any((e) => e.description == null)) {
                sb.AppendLine("<space=25em><color=#848484><b>Right-click to open all in browser</b></color>");
            } else {
                sb.AppendLine();
            }
            sb.AppendLine();
            foreach (var externalReference in entity.external_references) {
                if (externalReference.description == null) {
                    sb.AppendFormat(
                        "<indent=2em>\U00002014<indent=7em><b>{0}</b> (from <color=#96c8e9>{1}</color>)",
                        externalReference.id,
                        externalReference.url.ExtractUrlDomain());
                } else {
                    sb.AppendFormat("<i><b><indent=7em>{0}</b></i>", externalReference.description);
                }
                sb.AppendLine("<indent=0>");
            }
        }

        private static string buildAttributeText(string label, string text) {
            return string.Format(
                "<indent=2em>\U00002014<indent=7em><b>{0}:</b> {1}",
                label,
                !string.IsNullOrEmpty(text) ? text : "-");
        }

        private static string buildInjectionVectorText(AttackInjectionVector vector) {
            return string.Format("{0} <i>({1})</i>", vector.description, vector.categories.Join(", "));
        }

        private static string buildActivationZoneText(AttackActivationZone zone) {
            return string.Format("{0} <i>({1})</i>", zone.description, zone.categories.Join(", "));
        }

        private static string buildDamageText(DamageAttribute damage, float fireRate) {
            return (string.Format("{0:#0.0}-{1:#0.0}", damage.MinimumDamage, damage.MaximumDamage).PadLeft(10)
                    + string.Format(" ({0:##0.0} {1})", damage.GetDamagePerSecond(fireRate), "DPS".Monospaced(2.5f)))
                   .Monospaced()
                   .Indented(25);
        }

        private static string buildRangeText(float range) {
            return string.Format("{0:F1}", range).PadLeft(10).Monospaced().Indented(25);
        }

        private static string buildFireRateText(float fireRate) {
            return string.Format("{0:F1}", fireRate).PadLeft(10).Monospaced().Indented(25);
        }

        private static string buildImpactText(AttackImpact impact) {
            return string.Format(
                "Confidentiality: {0}, Integrity: {1}, Availability: {2}",
                impact.confidentiality,
                impact.integrity,
                impact.availability);
        }

    }

}
