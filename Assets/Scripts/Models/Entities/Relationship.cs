using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// ReSharper disable InconsistentNaming

namespace DdSG {

    [Serializable]
    public class Relationship: StixEntityBase {

        [JsonConverter(typeof(StringEnumConverter))]
        public StixRelationshipType relationship_type;
        [JsonConverter(typeof(StixIdConverter))]
        public StixId source_ref;
        [JsonConverter(typeof(StixIdConverter))]
        public StixId target_ref;

        [JsonIgnore]
        public StixId ParentAssetId { get; set; }

    }

}
