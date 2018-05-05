using System;
using System.Diagnostics.CodeAnalysis;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class EntitiesJson: TransferObjectBase {

        public DateTime created;
        public Entities entities;

    }

}
