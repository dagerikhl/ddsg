using System.Collections.Generic;
using System.Linq;

namespace DdSG {

    public static class AttackPatternExtensions {

        public static AttackPattern WithReferenceToParentAsset(this AttackPattern attackPattern, StixId assetId) {
            attackPattern.ParentAssetId = assetId;
            return attackPattern;
        }

        public static AttackPattern WithReferenceToMitigatedByCategories(
            this AttackPattern attackPattern,
            IEnumerable<CourseOfAction> courseOfActions,
            IEnumerable<Relationship> relationships) {
            // TODO
            var categories = new List<string>();
            foreach (var relationship in relationships) {
                foreach (var courseOfAction in courseOfActions) {
                    if (relationship.relationship_type == StixRelationshipType.Mitigates
                        && relationship.source_ref == courseOfAction.id
                        && relationship.target_ref == attackPattern.id) {
                        categories.Add(courseOfAction.custom.category);
                    }
                }
            }

            attackPattern.MitigatedByCategories = categories.Distinct().ToArray();

            return attackPattern;
        }

        public static AttackPattern TakeRandomByLikelihood(this AttackPattern[] attackPatterns) {
            return attackPatterns.TakeRandomsByLikelihood().FirstOrDefault();
        }

        public static AttackPattern[] TakeRandomsByLikelihood(this AttackPattern[] attackPatterns, int count = 1) {
            var randomAttackPatterns = new List<AttackPattern>(count);
            while (randomAttackPatterns.Count < count) {
                var randomAttackPattern = attackPatterns.TakeRandom();

                var chance = randomAttackPattern.CalculateSpawnLikelihoodFromLikelihood(1f);
                var threshold = Rnd.Gen.NextDouble();
                if (chance >= threshold) {
                    randomAttackPatterns.Add(randomAttackPattern);
                }
            }

            return randomAttackPatterns.ToArray();
        }

        public static float CalculateHealthFromSeverity(this AttackPattern attackPattern, float baseValue) {
            return baseValue + attackPattern.custom.severity.AsFloat()*baseValue;
        }

        public static float CalculateSpawnLikelihoodFromLikelihood(
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

        public static float CalculateDamageToAssetFromImpact(this AttackPattern attackPattern, float baseValue) {
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
