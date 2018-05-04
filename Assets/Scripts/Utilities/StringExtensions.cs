using System.Text;

namespace DdSG {

    public static class StringExtensions {

        public static string Repeat(this char value, int count) {
            return new string(value, count);
        }

        public static string Repeat(this string value, int count) {
            return new StringBuilder(value.Length*count).Insert(0, value, count).ToString();
        }

    }

}
