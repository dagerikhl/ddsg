using System.Linq;
using System.Text;

namespace DdSG {

    public static class Formatter {

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
            return "Click to " + text;
        }

    }

}
