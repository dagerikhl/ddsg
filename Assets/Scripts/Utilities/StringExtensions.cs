using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DdSG {

    public static class StringExtensions {

        public static string Repeat(this string value, int count) {
            return new StringBuilder(value.Length*count).Insert(0, value, count).ToString();
        }

        public static string[] Split(this string value, string separator) {
            return value.Split(new string[] { separator }, StringSplitOptions.None);
        }

        public static string PascalToKebabCase(this string value) {
            return string.IsNullOrEmpty(value) ? value
                : Regex.Replace(value, "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", "-$1").Trim().ToLower();
        }

        public static string KebabToPascalCase(this string value) {
            string[] words = value.Split(new char[] { '-', '_' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder sb = new StringBuilder(words.Sum(x => x.Length));

            foreach (string word in words) {
                sb.Append(word[0].ToString().ToUpper() + word.Substring(1));
            }

            return sb.ToString();
        }

        public static string Monospaced(this string value, float monospaceEm = 2f) {
            return string.Format("<mspace={0}em>{1}</mspace>", monospaceEm, value);
        }

        public static string CurrencyFormat(this string value) {
            return string.Format("{0:###,##0}", Convert.ToInt32(value));
        }

    }

}
