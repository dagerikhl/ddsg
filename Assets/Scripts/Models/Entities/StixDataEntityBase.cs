using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public abstract class StixDataEntityBase: StixEntityBase {

        public string[] description;
        public ExternalReference[] external_references;

        [JsonIgnore]
        public string FullDescription { get { return string.Join(" ", description); } }

    }

}
