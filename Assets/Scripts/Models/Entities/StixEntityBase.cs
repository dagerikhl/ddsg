using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// ReSharper disable InconsistentNaming

namespace DdSG {

    [Serializable]
    public abstract class StixEntityBase: TransferObjectBase {

        [JsonConverter(typeof(StringEnumConverter))]
        public StixType type;
        [JsonConverter(typeof(StixIdConverter))]
        public StixId id;
        public DateTime created;
        public DateTime modified;

    }

}
