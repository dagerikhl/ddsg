using System;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace DdSG {

    [Serializable]
    public abstract class StixDataEntityBase: StixEntityBase {

        public string[] description;
        public ExternalReference[] external_references;

        [JsonIgnore]
        public string FullDescription { get { return string.Join(" ", description); } }

    }

}
