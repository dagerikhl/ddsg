// ReSharper disable InconsistentNaming

using System.Linq;

namespace DdSG {

    public static class StixRelationshipObjectsExtensions {

        public static StixRelationshipObjects WithAllChildrenOfAssetRemoved(
            this StixRelationshipObjects SROs,
            StixId assetId) {
            SROs.relationships = SROs.relationships.Where((r) => r.ParentAssetId != assetId).ToArray();

            return SROs;
        }

    }

}
