using System.Collections.Generic;

namespace DdSG {

    public static class EntitiesExtensions {

        public static AttackPattern[] TakeRandomsByLikelihood(this AttackPattern[] attackPatterns, int count) {
            var randomAttackPatterns = new List<AttackPattern>(count);
            while (randomAttackPatterns.Count < count) {
                var randomAttackPattern = attackPatterns.TakeRandom();

                var chance = randomAttackPattern.calculateSpawnLikelihoodFromLikelihood(1f);
                var threshold = Rnd.Gen.NextDouble();
                if (chance >= threshold) {
                    randomAttackPatterns.Add(randomAttackPattern);
                }
            }

            return randomAttackPatterns.ToArray();
        }

        public static float calculateHealthFromSeverity(this AttackPattern attackPattern, float baseValue) {
            return baseValue + attackPattern.custom.severity.AsFloat()*baseValue;
        }

        public static float calculateSpawnLikelihoodFromLikelihood(
            this AttackPattern attackPattern,
            float baseValue,
            float minChance = 0f,
            float maxChance = 1f) {
            if (attackPattern.custom.likelihood == null) {
                return baseValue;
            }

            var likelihoodPart = attackPattern.custom.likelihood.AsPart();
            return MathHelper.Rangify(likelihoodPart, minChance, maxChance);
        }

        public static float calculateDamageToAssetFromImpact(this AttackPattern attackPattern, float baseValue) {
            if (attackPattern.custom.impact == null) {
                return baseValue;
            }

            var averageScale = (attackPattern.custom.impact.availability.AsPart()
                                + attackPattern.custom.impact.confidentiality.AsPart()
                                + attackPattern.custom.impact.integrity.AsPart())
                               /3f;

            return baseValue + averageScale*9f;
        }

    }

}
