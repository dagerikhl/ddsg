using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DdSG {

    [Serializable]
    public class StixId: TransferObjectBase {

        [JsonConverter(typeof(StringEnumConverter))]
        public StixType Type;
        public string Id;

        public string GetStixIdString() {
            return Enum.GetName(typeof(StixType), Type).PascalToKebabCase() + "--" + Id;
        }

    }

}
