using System;
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

    public static class ScaleExtensions {

        public static int AsInt(this Scale? scale) {
            if (scale == null) {
                return 0;
            }

            return (int) scale;
        }

        public static float AsFloat(this Scale? scale) {
            if (scale == null) {
                return 0f;
            }

            return (float) scale;
        }

        public static float AsPart(this Scale? scale) {
            if (scale == null) {
                return 0f;
            }

            return ((float) scale)/(Enum.GetValues(typeof(Scale)).Length - 1);
        }

    }

}
