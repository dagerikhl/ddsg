using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace DdSG {

    public static class EnumHelper {

        public static string GetEnumMemberAttributeValue<T>(T enumVal) {
            var enumType = typeof(T);
            MemberInfo[] memInfo = enumType.GetMember(enumVal.ToString());
            var memberInfo = memInfo.FirstOrDefault();

            EnumMemberAttribute attribute = null;
            if (memberInfo != null) {
                attribute = memberInfo.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
            }

            return attribute != null ? attribute.Value : null;
        }

    }

}
