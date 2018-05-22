using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DdSG {

    [Serializable]
#pragma warning disable 660,661
    public class StixId: TransferObjectBase {
#pragma warning restore 660,661

        [JsonConverter(typeof(StringEnumConverter))]
        public StixType Type;
        public string Id;

        public string GetStixIdString() {
            return Enum.GetName(typeof(StixType), Type).PascalToKebabCase() + "--" + Id;
        }

        public static bool operator ==(StixId lhs, StixId rhs) {
            return (object) lhs != null && (object) rhs != null && string.Equals(lhs.Id, rhs.Id);
        }

        public static bool operator !=(StixId lhs, StixId rhs) {
            return !(lhs == rhs);
        }

    }

}
