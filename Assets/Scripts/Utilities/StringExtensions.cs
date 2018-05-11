using System;
using System.Text;

namespace DdSG {

    public static class StringExtensions {

        public static string Repeat(this string value, int count) {
            return new StringBuilder(value.Length*count).Insert(0, value, count).ToString();
        }

        public static string[] Split(this string value, string separator) {
            return value.Split(new string[] { separator }, StringSplitOptions.None);
        }

    }

}
