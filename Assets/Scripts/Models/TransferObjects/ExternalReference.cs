using System;
using System.Diagnostics.CodeAnalysis;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExternalReference: TransferObjectBase {

        public string source_name;
        public string id;
        public string url;

    }

}
