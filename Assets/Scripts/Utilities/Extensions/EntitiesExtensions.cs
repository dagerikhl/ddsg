namespace DdSG {

    public static class EntitiesExtensions {

        public static float calculateHealthFromSeverity(this AttackPattern attackPattern, float baseValue) {
            return baseValue + attackPattern.custom.severity.AsFloat()*baseValue;
        }

        public static float calculateSpawnLikelihoodFromLikelihood(this AttackPattern attackPattern, float baseValue) {
            if (attackPattern.custom.likelihood == null) {
                return baseValue;
            }

            var likelihoodPart = attackPattern.custom.likelihood.AsPart();
            return MathHelper.Rangify(likelihoodPart, 0.5f, 1f);
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
