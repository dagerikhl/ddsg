using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class AttackBehaviour: MonoBehaviour {

        [Header("Attributes")]
        public float StartSpeed = 10f;
        public float StartHealth = 100f;
        public int StartValue = 10;
        public float DamageToAsset = 1f;

        [Header("Unity Setup Fields")]
        public HoverBehaviour HoverBehaviour;
        public ActionEvents ActionEvents;

        public Transform Model;

        public Canvas HealthBar;
        public Image HealthBarImage;

        public GameObject DeathEffect;

        // Public members hidden from Unity Inspector
        [HideInInspector]
        public AttackPattern AttackPattern;

        [HideInInspector]
        public float Speed;
        [HideInInspector]
        public float Health;

        public PathCategory InjectionVector { get; private set; }
        public AssetCategory ActivationZone { get; private set; }
        public AssetBehaviour TargetedAsset { get; private set; }
        public Transform SpawnPoint { get; private set; }

        public bool Invulnerable {
            get { return invulnerable; }
            set {
                invulnerable = value;

                foreach (Transform child in Model) {
                    child.GetComponent<Renderer>().material.color = Color.gray;
                }
                HealthBarImage.sprite = null;
                HealthBarImage.color = Color.gray;
            }
        }

        // Private and protected members
        private int value;

        private bool invulnerable;
        private bool isDead;

        private void Start() {
            Speed = StartSpeed;
            Health = StartHealth;
        }

        private void Update() {
            HealthBar.transform.rotation = CameraHelper.GetCameraRotationXy();
        }

        public void Initialize(AttackPattern attackPattern) {
            AttackPattern = attackPattern;

            InjectionVector = attackPattern.custom.injection_vector.categories.TakeRandom();
            ActivationZone = attackPattern.custom.activation_zone.categories.TakeRandom();
            TargetedAsset = FindObjectsOfType<AssetBehaviour>().Where((a) => a.Category == ActivationZone).TakeRandom();
            SpawnPoint = SpawnPoints.GetSpawnPoint(InjectionVector);

            transform.position = SpawnPoint.position;

            StartHealth = Health = attackPattern.CalculateHealthFromSeverity(StartHealth);
            DamageToAsset = attackPattern.CalculateDamageToAssetFromImpact(DamageToAsset);

            value += Mathf.CeilToInt(StartHealth/100f) + Mathf.CeilToInt(DamageToAsset);

            // Tweak scale and speed after value to indicate which attacks are more important
            transform.localScale *= value/10f;
            Speed *= value/10f;

            // Hover and click actions
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
            if (Invulnerable) {
                return;
            }

            Health -= amount;
            HealthBarImage.fillAmount = Health/StartHealth;

            if (Health <= 0f && !isDead) {
                die();
            }
        }

        public void Slow(float factor) {
            if (Invulnerable) {
                return;
            }

            Speed = StartSpeed*(1f - factor);
        }

        public void DamageAsset() {
            var newIntegrity = Mathf.Max(PlayerStats.I.GetAssetIntegrity(TargetedAsset.AssetIndex) - DamageToAsset, 0);
            PlayerStats.I.SetAssetIntegrity(TargetedAsset.AssetIndex, newIntegrity);
        }

        private void die() {
            isDead = true;

            PlayerStats.I.UpdateStatsFromKill(StartValue);

            // SFX
            var effect = UnityHelper.Instantiate(DeathEffect, transform.position);
            Destroy(effect, 5f);

            WaveSpawner.AttacksAlive--;
            Destroy(gameObject);
        }

    }

}
