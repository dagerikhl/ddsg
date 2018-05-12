using System.Collections.Generic;
using System.Linq;

namespace DdSG {

    public static class EnumerableExtensions {

        public static string Join<T>(this IEnumerable<T> value, string separator) {
            return string.Join(separator, value.Select((e) => e.ToString()).ToArray());
        }

        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> value, int numberOfElements = 1) {
            IEnumerable<T> valueAsArray = value as T[] ?? value.ToArray();

            var elements = new List<T>();
            for (int i = 0; i < numberOfElements; i++) {
                var index = Rnd.Gen.Next(valueAsArray.Count());
                elements.Add(valueAsArray.ElementAt(index));
            }

            return elements.AsEnumerable();
        }

    }

}
