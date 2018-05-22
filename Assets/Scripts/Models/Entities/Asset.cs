using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// ReSharper disable InconsistentNaming

namespace DdSG {

    [Serializable]
    public class Asset: StixDataEntityBase {

        public AssetCustoms custom;

        [JsonIgnore]
        public int AssetSocketIndex { get; set; }

    }

    [Serializable]
    public class AssetCustoms: TransferObjectBase {

        [JsonConverter(typeof(StringEnumConverter))]
        public AssetCategory category;

    }

}
