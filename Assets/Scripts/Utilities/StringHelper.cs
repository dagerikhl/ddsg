using System.Text;

namespace DdSG {

    public static class StringHelper {

        public static string Repeat(char value, int count) {
            return new string(value, count);
        }

        public static string Repeat(string value, int count) {
            return new StringBuilder(value.Length*count).Insert(0, value, count).ToString();
        }

    }

}
