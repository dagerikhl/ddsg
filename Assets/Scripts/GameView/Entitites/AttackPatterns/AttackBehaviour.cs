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
        public ClickableBehaviour ClickableBehaviour;

        public Transform Model;

        public Canvas HealthBar;
        public Image HealthBarImage;

        public GameObject DeathEffect;

        // Public members hidden from Unity Inspector
        [HideInInspector]
        public AttackPattern AttackPattern;

        private float health;
        private float Health {
            get { return health; }
            set {
                health = value;

                HealthBarImage.fillAmount = value/StartHealth;
            }
        }

        [HideInInspector]
        public float Speed;

        public PathCategory InjectionVector { get; private set; }
        public AssetCategory ActivationZone { get; private set; }
        public int TargetedAssetIndex { get; private set; }
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

        private void Awake() {
            StartHealth *= WaveSpawner.I.WaveIndex*0.5f;

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
            TargetedAssetIndex = AssetSockets.Assets.Select((a, i) => a.WithAssetSocketIndex(i))
                                             .Where((a) => a != null && a.custom.category == ActivationZone)
                                             .TakeRandom()
                                             .AssetSocketIndex;
            SpawnPoint = SpawnPoints.GetSpawnPoint(InjectionVector);

            transform.position = SpawnPoint.position;

            StartHealth = Health = attackPattern.CalculateHealthFromSeverity(StartHealth);
            Health *= State.I.PlayConfiguration.Difficulty;
            DamageToAsset = attackPattern.CalculateDamageToAssetFromImpact(DamageToAsset);

            value += Mathf.CeilToInt(StartHealth/WaveSpawner.I.WaveIndex*0.5f/100f) + Mathf.CeilToInt(DamageToAsset);

            // Tweak scale and speed after value to indicate which attacks are more important
            Model.localScale *= value/10f;
            Speed *= value/10f;

            // Hover and click actions
            ClickableBehaviour.Title = attackPattern.name;
            ClickableBehaviour.Text = Formatter.BuildStixDataEntityDescription(attackPattern);

            ClickableBehaviour.ActionText = "select";
            ClickableBehaviour.PrimaryAction = () => {
                SelectionHelper.DeselectAllMitigations();

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
            ClickableBehaviour.HasSecondaryAction =
                ReferencesHelper.AddReferencesAsAction(attackPattern, ClickableBehaviour);
        }

        public void TakeDamage(float amount) {
            if (Invulnerable) {
                return;
            }

            Health -= amount;

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
            if (TargetedAssetIndex != -1) {
                AssetSockets.AssetBehaviours[TargetedAssetIndex].TakeDamage(DamageToAsset);
            }
        }

        private void die() {
            isDead = true;

            PlayerStats.I.UpdateStatsForKilledAttack(StartValue);

            // SFX
            var effect = UnityHelper.Instantiate(DeathEffect, transform.position);
            Destroy(effect, 5f);

            WaveSpawner.I.AttacksAlive--;
            Destroy(gameObject);
        }

    }

}
