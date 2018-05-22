using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class AssetBehaviour: MonoBehaviour {

        [Header("Attributes")]
        public int Value;

        [Header("Unity Setup Fields")]
        public ClickableBehaviour ClickableBehaviour;

        public Canvas HealthBar;
        public Image HealthBarImage;

        public GameObject DeathEffect;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        [HideInInspector]
        public AssetCategory Category;
        [HideInInspector]
        public int AssetIndex;

        // Private and protected members
        private float integrity;

        private bool isDead;

        private void Update() {
            integrity = PlayerStats.I.GetAssetIntegrity(AssetIndex);

            HealthBar.transform.rotation = CameraHelper.GetCameraRotationXy();
            HealthBarImage.fillAmount = integrity/100f;

            if (integrity <= 0f && !isDead) {
                die();
            }
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

        private void die() {
            isDead = true;

            PlayerStats.I.UpdateStatsForLostAsset(Value);

            // SFX
            var effect = UnityHelper.Instantiate(DeathEffect, transform.position);
            Destroy(effect, 5f);

            Destroy(gameObject);
        }

    }

}
