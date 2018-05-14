using UnityEngine;

namespace DdSG {

    public static class ColorExtensions {

        public static Color WithAlpha(this Color color, float alpha) {
            var newColor = color;
            newColor.a = alpha;

            return newColor;
        }

    }

}
