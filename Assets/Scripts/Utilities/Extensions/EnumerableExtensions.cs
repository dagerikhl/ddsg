using System;
using System.Collections.Generic;
using System.Linq;

namespace DdSG {

    public static class EnumerableExtensions {

        public static string Join<T>(this IEnumerable<T> value, string separator) {
            if (value == null) {
                return "";
            }

            var joinedValues = value.Where((e) => e != null).Select((e) => e.ToString());

            return string.Join(separator, joinedValues.ToArray());
        }

        public static T TakeRandom<T>(this IEnumerable<T> value) {
            return value.TakeRandoms().FirstOrDefault();
        }

        public static IEnumerable<T> TakeRandoms<T>(this IEnumerable<T> value, int numberOfElements = 1) {
            IEnumerable<T> valueAsArray = value as T[] ?? value.ToArray();

            var elements = new List<T>();
            for (int i = 0; i < numberOfElements; i++) {
                var index = Rnd.Gen.Next(valueAsArray.Count());
                elements.Add(valueAsArray.ElementAt(index));
            }

            return elements.AsEnumerable();
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector) {
            var seenKeys = new HashSet<TKey>();
            foreach (T element in source) {
                if (seenKeys.Add(keySelector(element))) {
                    yield return element;
                }
            }
        }

    }

}
