using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class AttackBehaviour: MonoBehaviour {

        [Header("Attributes")]
        public float StartSpeed = 10f;
        public float StartHealth = 100f;
        public int Worth = 50;

        [Header("Unity Setup Fields")]
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

        // Private and protected members
        private bool isDead;

        private void Start() {
            Speed = StartSpeed;
        }

        private void Update() {
            HealthBar.transform.rotation = HelperObjects.Camera.rotation;
        }

        public void Initialize(AttackPattern attackPattern) {
            InjectionVector = attackPattern.custom.injection_vector.categories.TakeRandom();
            ActivationZone = attackPattern.custom.activation_zone.categories.TakeRandom();
        }

        public void Damage(float amount) {
            Health -= amount;
            HealthBarImage.fillAmount = Health/StartHealth;

            if (Health <= 0f && !isDead) {
                die();
            }
        }

        public void Slow(float factor) {
            Speed = StartSpeed*(1f - factor);
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
