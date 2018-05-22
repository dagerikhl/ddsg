namespace DdSG {

    public static class RelationshipExtensions {

        public static Relationship WithReferenceToParentAsset(this Relationship relationship, StixId assetId) {
            relationship.ParentAssetId = assetId;
            return relationship;
        }

    }

}
