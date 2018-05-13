using System;
using System.Text;

// ReSharper disable InconsistentNaming

namespace DdSG {

    [Serializable]
    public class ExternalReference: TransferObjectBase {

        public string source_name;
        public string id;
        public string url;
        public string description;

        public override string ToString() {
            var sb = new StringBuilder();

            if (description != null) {
                sb.Append("Source: " + description);
            } else {
                sb.Append("Source: " + id);
                sb.Append(" <i><color=#365899>(" + url + ")</color></i>");
            }

            return sb.ToString();
        }

    }

}
