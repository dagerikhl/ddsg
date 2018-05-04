using System;
using System.Diagnostics.CodeAnalysis;

namespace DdSG {

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Relationship: TransferObject {

        public StixType type;
        public string id;
        public string created;
        public string modified;
        public StixRelationshipType relationship_type;
        public string source_ref;
        public string target_ref;

    }

}
