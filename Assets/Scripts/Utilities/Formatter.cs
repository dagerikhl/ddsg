using System;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DdSG {

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

        public static string WaveCounterFormat(int waveNumber) {
            var waveString = waveNumber == 0 ? "-" : string.Format("{0:#0}", waveNumber);
            return waveString.PadLeft(2);
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

            if (showSources) {
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

            return sb.ToString();
        }

        public static string BuildActionText(string text) {
            return !string.IsNullOrEmpty(text) ? "Click to " + text : "";
        }

        private static void appendAttributes(StringBuilder sb, StixDataEntityBase entity, bool showDescription) {
            var hasAttributes = entity.GetType() == typeof(Asset)
                                || entity.GetType() == typeof(AttackPattern)
                                || entity.GetType() == typeof(CourseOfAction);

            if (!hasAttributes) {
                return;
            }

            if (showDescription) {
                sb.AppendLine("<i><size=140%>Attributes</size></i>");
                sb.AppendLine();
            }

            if (entity.GetType() == typeof(Asset)) {
                appendAssetAttributes(sb, (Asset) entity);
            } else if (entity.GetType() == typeof(AttackPattern)) {
                appendAttackPatternAttributes(sb, (AttackPattern) entity);
            } else if (entity.GetType() == typeof(CourseOfAction)) {
                appendCourseOfActionAttributes(sb, (CourseOfAction) entity);
            }
        }

        private static void appendAssetAttributes(StringBuilder sb, Asset asset) {
            // Doesn't have any attributes worth showing yet; kept for future use
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

            sb.AppendLine("<indent=0>");
        }

        private static void appendCourseOfActionAttributes(StringBuilder sb, CourseOfAction courseOfAction) {
            // TODO Add attributes for mitigations

            sb.AppendLine("<indent=0>");
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

        private static string buildImpactText(AttackImpact impact) {
            return string.Format(
                "Confidentiality: {0}, Integrity: {1}, Availability: {2}",
                impact.confidentiality,
                impact.integrity,
                impact.availability);
        }

    }

}
