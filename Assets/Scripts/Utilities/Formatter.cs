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
            var timeString = seconds < 0.01f ? "-.--" : string.Format("{0:##0.00}", seconds);
            return timeString.PadLeft(6);
        }

        public static string WaveCounterFormat(int waveNumber) {
            var waveString = waveNumber == 0 ? "-" : string.Format("{0:#0}", waveNumber);
            return waveString.PadLeft(2);
        }

        public static string BuildStixDataEntityDescription(StixDataEntityBase entity) {
            var sb = new StringBuilder();

            sb.AppendLine(entity.FullDescription);
            sb.AppendLine();

            if (entity.GetType() == typeof(Asset)) {
                AppendAssetAttributes(sb, (Asset) entity);
            } else if (entity.GetType() == typeof(AttackPattern)) {
                AppendAttackPatternAttributes(sb, (AttackPattern) entity);
            } else if (entity.GetType() == typeof(CourseOfAction)) {
                AppendCourseOfActionAttributes(sb, (CourseOfAction) entity);
            }

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

            return sb.ToString();
        }

        public static string BuildActionText(string text) {
            return !string.IsNullOrEmpty(text) ? "Click to " + text : "";
        }

        private static void AppendAssetAttributes(StringBuilder sb, Asset asset) {
            // Doesn't have any attributes worth showing yet; kept for future use
        }

        private static void AppendAttackPatternAttributes(StringBuilder sb, AttackPattern attackPattern) {
            sb.AppendLine("<i><size=140%>Attributes</size></i>");
            sb.AppendLine();

            if (attackPattern.custom.severity != null) {
                sb.AppendLine(
                    BuildAttributeText(
                        "Severity",
                        EnumHelper.GetEnumMemberAttributeValue((Scale) attackPattern.custom.severity)));
            }
            if (attackPattern.custom.likelihood != null) {
                sb.AppendLine(
                    BuildAttributeText(
                        "Likelihood",
                        EnumHelper.GetEnumMemberAttributeValue((Scale) attackPattern.custom.likelihood)));
            }
            sb.AppendLine(
                BuildAttributeText(
                    "Injection Vector",
                    BuildInjectionVectorText(attackPattern.custom.injection_vector)));
            sb.AppendLine(BuildAttributeText("Payload", attackPattern.custom.payload));
            sb.AppendLine(
                BuildAttributeText("Activation Zone", BuildActivationZoneText(attackPattern.custom.activation_zone)));
            sb.AppendLine(BuildAttributeText("Impact", BuildImpactText(attackPattern.custom.impact)));

            sb.AppendLine("<indent=0>");
        }

        private static void AppendCourseOfActionAttributes(StringBuilder sb, CourseOfAction courseOfAction) {
            sb.AppendLine("<i><size=140%>Attributes</size></i>");
            sb.AppendLine();

            // TODO Add attributes for mitigations

            sb.AppendLine("<indent=0>");
        }

        private static string BuildAttributeText(string label, string text) {
            return string.Format(
                "<indent=2em>\U00002014<indent=7em><b>{0}:</b> {1}",
                label,
                !string.IsNullOrEmpty(text) ? text : "-");
        }

        private static string BuildInjectionVectorText(AttackInjectionVector vector) {
            return string.Format("{0} <i>({1})</i>", vector.description, vector.categories.Join(", "));
        }

        private static string BuildActivationZoneText(AttackActivationZone zone) {
            return string.Format("{0} <i>({1})</i>", zone.description, zone.categories.Join(", "));
        }

        private static string BuildImpactText(AttackImpact impact) {
            return string.Format(
                "Confidentiality: {0}, Integrity: {1}, Availability: {2}",
                impact.confidentiality,
                impact.integrity,
                impact.availability);
        }

    }

}
