using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Asset: StixEntityBase {

        public string[] description;
        public ExternalReference[] external_references;
        public AssetCustoms custom;

        [JsonIgnore]
        public string FullDescription { get { return string.Join(" ", description); } }

    }

}
