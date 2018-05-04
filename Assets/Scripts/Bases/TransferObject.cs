using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace DdSG {

    [Serializable]
    public abstract class TransferObject {

        private static int depth;

        public override string ToString() {
            FieldInfo[] fieldInfos = GetType().GetFields();

            var sb = new StringBuilder();

            if (fieldInfos.Length > 0) {
                sb.AppendLine("{");
                depth++;
            }
            foreach (var info in fieldInfos) {
                var value = info.GetValue(this) ?? "(null)";

                if (value.GetType().IsArray) {
                    sb.AppendLine(indent() + info.Name + ": [");
                    depth++;
                    foreach (var element in (IEnumerable) value) {
                        sb.AppendLine(indent() + element);
                    }
                    depth--;
                    sb.AppendLine(indent() + "]");
                } else {
                    sb.AppendLine(indent() + info.Name + ": " + value);
                }
            }
            if (fieldInfos.Length > 0) {
                depth--;
                sb.AppendLine(indent() + "}");
            }

            return sb.ToString();
        }

        private string indent() {
            return StringHelper.Repeat("  ", depth);
        }

    }

}
