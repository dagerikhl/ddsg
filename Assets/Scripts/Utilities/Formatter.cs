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

        // TODO Create a custom one for attack patterns to show more custom data
        public static string BuildStixDataEntityDescription(StixDataEntityBase entity) {
            var sb = new StringBuilder();

            sb.AppendLine(entity.FullDescription);
            sb.AppendLine();
            sb.Append("<i>Sources</i>");
            if (entity.external_references.Any((e) => e.description == null)) {
                sb.AppendLine("<space=30em><color=#848484><b>Right-click to open all in browser</b></color>");
            } else {
                sb.AppendLine();
            }
            sb.AppendLine();
            foreach (var externalReference in entity.external_references) {
                if (externalReference.description == null) {
                    sb.AppendFormat(
                        "<i><b><indent=2em>\U00002014<indent=7em>{0}</b> (from <color=#96c8e9>{1}</color>)</i>",
                        externalReference.id,
                        externalReference.url.ExtractUrlDomain());
                } else {
                    sb.AppendFormat("<i><b><indent=7em>{0}</b></i>", externalReference.description);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public static string BuildActionText(string text) {
            return !string.IsNullOrEmpty(text) ? "Click to " + text : "";
        }

    }

}
