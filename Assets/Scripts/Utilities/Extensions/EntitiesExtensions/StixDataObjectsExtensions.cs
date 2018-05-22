// ReSharper disable InconsistentNaming

using System.Linq;

namespace DdSG {

    public static class StixDataObjectsExtensions {

        public static StixDataObjects WithAllChildrenOfAssetRemoved(this StixDataObjects SDOs, StixId assetId) {
            SDOs.assets = SDOs.assets.Where((a) => a.id != assetId).ToArray();

            SDOs.attack_patterns = SDOs.attack_patterns.Where((aP) => aP.ParentAssetId != assetId).ToArray();

            return SDOs;
        }

    }

}
