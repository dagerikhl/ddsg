using System;
using System.Diagnostics.CodeAnalysis;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class EntitiesJson: TransferObjectBase {

        public string created;
        public Entities entities;

    }

}
