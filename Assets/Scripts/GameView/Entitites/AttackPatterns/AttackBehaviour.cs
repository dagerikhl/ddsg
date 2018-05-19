using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class AttackBehaviour: MonoBehaviour {

        [Header("Attributes")]
        public float StartSpeed = 10f;
        public float StartHealth = 100f;
        public int Worth = 50;
        public float DamageToAsset = 1f;

        [Header("Unity Setup Fields")]
        public HoverBehaviour HoverBehaviour;
        public ActionEvents ActionEvents;

        public Canvas HealthBar;
        public Image HealthBarImage;
        public GameObject DeathEffect;

        // Public members hidden from Unity Inspector
        [HideInInspector]
        public float Speed;
        [HideInInspector]
        public float Health;

        public PathCategory InjectionVector { get; private set; }
        public AssetCategory ActivationZone { get; private set; }
        public AssetBehaviour TargetedAsset { get; private set; }
        public Transform SpawnPoint { get; private set; }

        // Private and protected members
        private bool isDead;

        private void Start() {
            Speed = StartSpeed;
            Health = StartHealth;
        }

        private void Update() {
            HealthBar.transform.rotation = CameraHelper.GetCameraRotationXy();
        }

        public void Initialize(AttackPattern attackPattern) {
            InjectionVector = attackPattern.custom.injection_vector.categories.TakeRandom();
            ActivationZone = attackPattern.custom.activation_zone.categories.TakeRandom();
            TargetedAsset = FindObjectsOfType<AssetBehaviour>().Where((a) => a.Category == ActivationZone).TakeRandom();
            SpawnPoint = SpawnPoints.GetSpawnPoint(InjectionVector);

            transform.position = SpawnPoint.position;

            StartHealth = Health = attackPattern.CalculateHealthFromSeverity(StartHealth);
            DamageToAsset = attackPattern.CalculateDamageToAssetFromImpact(DamageToAsset);
            // TODO Balance these values
            // Logger.Debug(string.Format("{0}, {1}, {2}", Health, SpawnLikelihood, DamageToAsset));

            HoverBehaviour.Title = attackPattern.name;
            HoverBehaviour.Text = Formatter.BuildStixDataEntityDescription(attackPattern);

            HoverBehaviour.ActionText = "select";
            ActionEvents.PrimaryAction = () => {
                var title = attackPattern.name;
                var description = Formatter.BuildStixDataEntityDescription(attackPattern, false, false);
                SelectedAction[] selectedActions = ReferencesHelper.HasExternalReferences(attackPattern)
                    ? new SelectedAction[] {
                        new SelectedAction(
                            ActionType.OpenExternalReferences,
                            () => ReferencesHelper.OpenExternalReferences(attackPattern))
                    } : null;
                HelperObjects.SelectedInfoBar.SelectEntity(title, "Mitigation", description, selectedActions);
            };
            HoverBehaviour.HasSecondaryAction = ReferencesHelper.AddReferencesAsAction(attackPattern, ActionEvents);
        }

        public void TakeDamage(float amount) {
            Health -= amount;
            HealthBarImage.fillAmount = Health/StartHealth;

            if (Health <= 0f && !isDead) {
                die();
            }
        }

        public void Slow(float factor) {
            Speed = StartSpeed*(1f - factor);
        }

        public void DamageAsset() {
            var newIntegrity = Mathf.Max(PlayerStats.I.GetAssetIntegrity(TargetedAsset.AssetIndex) - DamageToAsset, 0);
            PlayerStats.I.SetAssetIntegrity(TargetedAsset.AssetIndex, newIntegrity);
        }

        private void die() {
            isDead = true;

            PlayerStats.I.Worth += Worth;

            // SFX
            var effect = UnityHelper.Instantiate(DeathEffect, transform.position);
            Destroy(effect, 5f);

            WaveSpawner.AttacksAlive--;
            Destroy(gameObject);
        }

    }

}
