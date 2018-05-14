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
            HoverBehaviour.Text = Formatter.BuildStixDataEntityDescription(asset);
        }

    }

}
