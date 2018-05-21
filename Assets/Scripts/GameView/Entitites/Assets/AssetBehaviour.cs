using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class AssetBehaviour: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public ClickableBehaviour ClickableBehaviour;

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

            ClickableBehaviour.Title = EnumHelper.GetEnumMemberAttributeValue(asset.custom.category);
            ClickableBehaviour.Text = Formatter.BuildStixDataEntityDescription(asset);

            ClickableBehaviour.ActionText = "select";
            ClickableBehaviour.PrimaryAction = () => {
                var title = EnumHelper.GetEnumMemberAttributeValue(asset.custom.category);
                var description = Formatter.BuildStixDataEntityDescription(asset, true, false);
                HelperObjects.SelectedInfoBar.SelectEntity(title, "Asset", description);
            };
            ClickableBehaviour.HasSecondaryAction = ReferencesHelper.AddReferencesAsAction(asset, ClickableBehaviour);
        }

    }

}
