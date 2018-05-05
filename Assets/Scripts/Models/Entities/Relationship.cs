using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Relationship: StixEntityBase {

        [JsonConverter(typeof(StringEnumConverter))]
        public StixRelationshipType relationship_type;
        [JsonConverter(typeof(StixIdConverter))]
        public StixId source_ref;
        [JsonConverter(typeof(StixIdConverter))]
        public StixId target_ref;

    }

}
