using System.Runtime.Serialization;

namespace DdSG {

    public enum StixRelationshipType {

        [EnumMember(Value = "mitigates")]
        Mitigates,
        [EnumMember(Value = "targets")]
        Targets

    }

}
