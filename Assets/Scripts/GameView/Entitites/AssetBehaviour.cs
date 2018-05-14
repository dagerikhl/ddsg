using System.Linq;
using UnityEngine;

namespace DdSG {

    public class AssetBehaviour: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public HoverBehaviour HoverBehaviour;
        public ClickBehaviour ClickBehaviour;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        public void Initialize(Asset asset) {
            HoverBehaviour.Title = asset.custom.category;
            HoverBehaviour.Text = Formatter.BuildStixDataEntityDescription(asset);

            if (asset.external_references.Any((e) => e.description == null)) {
                HoverBehaviour.HasSecondaryAction = true;
                ClickBehaviour.SecondaryAction = () => {
                    foreach (var url in asset.external_references.Select((e) => e.url)) {
                        FindObjectOfType<PauseMenu>().Pause();
                        Application.OpenURL(url);
                    }
                };
            }
        }

    }

}
