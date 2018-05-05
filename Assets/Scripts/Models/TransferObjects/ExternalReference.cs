using System;

// ReSharper disable InconsistentNaming

namespace DdSG {

    [Serializable]
    public class ExternalReference: TransferObjectBase {

        public string source_name;
        public string id;
        public string url;

    }

}
