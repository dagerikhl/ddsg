using System.Runtime.Serialization;

namespace DdSG {

    public enum StixType {

        [EnumMember(Value = "attack-pattern")]
        AttackPattern,
        [EnumMember(Value = "course-of-action")]
        CourseOfAction,
        [EnumMember(Value = "asset")]
        Asset,
        [EnumMember(Value = "relationship")]
        Relationship

    }

}
