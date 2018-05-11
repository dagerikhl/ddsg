using System.Collections.Generic;
using System.Linq;

namespace DdSG {

    public static class EnumerableExtensions {

        public static string Join<T>(this IEnumerable<T> value, string separator) {
            return string.Join(separator, value.Select((e) => e.ToString()).ToArray());
        }

    }

}
