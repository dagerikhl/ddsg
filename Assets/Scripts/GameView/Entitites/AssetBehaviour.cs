using System.Linq;
using UnityEngine;

namespace DdSG {

    public class AssetBehaviour: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public HoverBehaviour HoverBehaviour;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        public void Initialize(Asset asset) {
            HoverBehaviour.Title = asset.custom.category;
            HoverBehaviour.Text = buildAssetTooltip(asset);
        }

        // TODO Build a prettier and better tooltip
        // TODO Add func. to let users click the asset to be taken to CAPEC
        private string buildAssetTooltip(Asset asset) {
            return asset.FullDescription
                   + "\n\n"
                   + "References:\n"
                   + asset.external_references.Select((e) => "-<indent=5%>" + e).Join("\n");
        }

    }

}
