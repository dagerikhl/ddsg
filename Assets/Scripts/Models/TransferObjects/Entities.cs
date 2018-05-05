using System;
using System.Diagnostics.CodeAnalysis;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Entities: TransferObjectBase {

        public StixDataObjects SDOs;
        public StixRelationshipObjects SROs;

    }

}
