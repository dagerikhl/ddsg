using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class AssetBehaviour: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public HoverBehaviour HoverBehaviour;
        public ActionEvents ActionEvents;

        public Canvas HealthBar;
        public Image HealthBarImage;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        [HideInInspector]
        public AssetCategory Category;
        [HideInInspector]
        public int AssetIndex;

        // Private and protected members

        private void Update() {
            HealthBar.transform.rotation = CameraHelper.GetCameraRotationXy();
            HealthBarImage.fillAmount = PlayerStats.I.GetAssetIntegrity(AssetIndex)/100f;
        }

        public void Initialize(Asset asset, int assetIndex) {
            Category = asset.custom.category;
            AssetIndex = assetIndex;

            HoverBehaviour.Title = EnumHelper.GetEnumMemberAttributeValue(asset.custom.category);
            HoverBehaviour.Text = Formatter.BuildStixDataEntityDescription(asset);

            HoverBehaviour.ActionText = "select";
            ActionEvents.PrimaryAction = () => {
                var title = EnumHelper.GetEnumMemberAttributeValue(asset.custom.category);
                var description = Formatter.BuildStixDataEntityDescription(asset, false, false);
                HelperObjects.SelectedInfoBar.SelectEntity(title, description);
            };
            HoverBehaviour.HasSecondaryAction = ReferencesHelper.AddReferencesAsAction(asset, ActionEvents);
        }

    }

}
