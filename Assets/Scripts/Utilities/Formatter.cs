using System.Linq;
using System.Text;

namespace DdSG {

    public static class Formatter {

        public static string BuildStixDataEntityDescription(StixDataEntityBase entity) {
            var sb = new StringBuilder();

            sb.AppendLine(entity.FullDescription);
            sb.AppendLine("References:\n");
            sb.AppendLine(entity.external_references.Select((e) => "-<indent=5%>" + e).Join("\n"));

            return sb.ToString();
        }

        public static string BuildActionText(string text) {
            return "Click to " + text;
        }

    }

}
