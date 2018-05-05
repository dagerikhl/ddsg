using System;
using System.Diagnostics.CodeAnalysis;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class StixRelationshipObjects: TransferObjectBase {

        public Relationship[] relationships;

    }

}
