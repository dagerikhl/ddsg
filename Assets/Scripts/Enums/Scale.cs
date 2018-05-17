using System.Runtime.Serialization;

namespace DdSG {

    public enum Scale {

        [EnumMember(Value = "Very Low")]
        VeryLow,
        [EnumMember(Value = "Low")]
        Low,
        [EnumMember(Value = "Medium")]
        Medium,
        [EnumMember(Value = "High")]
        High,
        [EnumMember(Value = "Very High")]
        VeryHigh

    }

}
