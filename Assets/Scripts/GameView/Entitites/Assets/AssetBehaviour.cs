using System.Linq;
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
        private Asset asset;

        private bool isDead;

        private void Update() {
            HealthBar.transform.rotation = CameraHelper.GetCameraRotationXy();
        }

        public void Initialize(Asset asset, int assetIndex) {
            this.asset = asset;

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

        public void TakeDamage(float amount) {
            var oldIntegrity = PlayerStats.I.GetAssetIntegrity(AssetIndex);
            var newIntegrity = Mathf.Max(oldIntegrity - amount, 0);

            PlayerStats.I.SetAssetIntegrity(AssetIndex, newIntegrity);
            HealthBarImage.fillAmount = newIntegrity/100f;

            if (newIntegrity <= 0f && !isDead) {
                die();
            }
        }

        private void die() {
            isDead = true;

            PlayerStats.I.UpdateStatsForLostAsset(Value);

            // Update game state to not spawn attacks for this asset
            State.I.GameEntities.SDOs = State.I.GameEntities.SDOs.WithAllChildrenOfAssetRemoved(asset.id);
            State.I.GameEntities.SROs = State.I.GameEntities.SROs.WithAllChildrenOfAssetRemoved(asset.id);
            WaveSpawner.I.NextWave = WaveSpawner.I.GenerateNewWave();

            // SFX
            var effect = UnityHelper.Instantiate(DeathEffect, transform.position);
            Destroy(effect, 5f);

            Destroy(gameObject);
        }

    }

}
