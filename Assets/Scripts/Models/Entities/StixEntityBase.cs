using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public abstract class StixEntityBase: TransferObjectBase {

        [JsonConverter(typeof(StringEnumConverter))]
        public StixType type;
        [JsonConverter(typeof(StixIdConverter))]
        public StixId id;
        public DateTime created;
        public DateTime modified;

    }

}
