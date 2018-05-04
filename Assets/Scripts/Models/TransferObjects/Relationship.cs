using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Relationship: TransferObject {

        [JsonConverter(typeof(StringEnumConverter))]
        public StixType type;
        public string id;
        public string created;
        public string modified;
        [JsonConverter(typeof(StringEnumConverter))]
        public StixRelationshipType relationship_type;
        public string source_ref;
        public string target_ref;

    }

}
