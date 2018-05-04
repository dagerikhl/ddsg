using System;
using System.Diagnostics.CodeAnalysis;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class StixRelationshipObjects: TransferObject {

        public Relationship[] relationships;

    }

}
