using System;

namespace DdSG {

    public static class MathHelper {

        public static float Normalize(float value, float min, float max) {
            return (value - min)/(max - min);
        }

        public static float Rangify(float value, float min, float max) {
            if (value < 0f || value > 1f) {
                throw new ArgumentOutOfRangeException(
                    "value",
                    "Value must be normalized (between 0 - 1) before it can be rangified.");
            }

            return value*(max - min) + min;
        }

    }

}
