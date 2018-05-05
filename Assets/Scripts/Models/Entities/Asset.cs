using System;

// ReSharper disable InconsistentNaming

namespace DdSG {

    [Serializable]
    public class Asset: StixDataEntityBase {

        public AssetCustoms custom;

    }

    [Serializable]
    public class AssetCustoms: TransferObjectBase {

        public string category;

    }

}
