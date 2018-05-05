using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Asset: StixDataEntityBase {

        public AssetCustoms custom;

    }

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class AssetCustoms: TransferObjectBase {

        public string category;

    }

}
