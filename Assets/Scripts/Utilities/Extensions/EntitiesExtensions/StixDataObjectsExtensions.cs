// ReSharper disable InconsistentNaming

using System.Linq;

namespace DdSG {

    public static class StixDataObjectsExtensions {

        public static StixDataObjects WithAllChildrenOfAssetRemoved(this StixDataObjects SDOs, StixId assetId) {
            SDOs.assets = SDOs.assets.Where((a) => !string.Equals(a.id.Id, assetId.Id)).ToArray();

            SDOs.attack_patterns = SDOs.attack_patterns.Where((aP) => !string.Equals(aP.ParentAssetId.Id, assetId.Id))
                                       .ToArray();

            return SDOs;
        }

    }

}
