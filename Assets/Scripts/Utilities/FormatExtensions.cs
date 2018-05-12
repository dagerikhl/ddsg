﻿using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DdSG {

    public static class FormatExtensions {

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

        public static string WorthFormat(this string value) {
            return string.Format("{0:###,##0}", Convert.ToInt32(value));
        }

        public static string IntegrityFormat(this float value) {
            // var color = BitConverter.DoubleToInt64Bits(value*(255f/100f)).ToString("X6");
            // var color = Convert.ToDouble(value*(255f/100f)).ToString("X");
            var normalizedValue = value/100f;
            var color = string.Format(
                "#{0:X2}{1:X2}{2:X2}",
                (int) (255 - normalizedValue*255),
                (int) (normalizedValue*255),
                0);
            var valueString = string.Format("{0:0}", value).PadLeft(3, ' ');

            return string.Format("<color={0}>{1}</color><space=1em>%", color, valueString);
        }

    }

}