using UnityEngine;

namespace DdSG {

    public class Bullet: MonoBehaviour {

        [Header("Attributes")]
        public float Speed = 70f;
        public float Damage = 5f;

        [Header("Special Attacks (optional)")]
        public float ExplosionRadius;

        [Header("Unity Setup Fields")]
        public GameObject ImpactEffect;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector

        // Private and protected members
        private Transform target;

        private void Update() {
            if (target == null) {
                Destroy(gameObject);
                return;
            }

            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = Speed*Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame) {
                hitTarget();
                return;
            }

            transform.Translate(dir.normalized*distanceThisFrame, Space.World);
            transform.LookAt(target);
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
        }

        public void Seek(Transform newTarget) {
            target = newTarget;
        }

        private void hitTarget() {
            GameObject effectGo = UnityHelper.Instantiate(ImpactEffect, transform.position, transform.rotation);
            Destroy(effectGo, 5f);

            if (ExplosionRadius > 0f) {
                explode();
            } else {
                damage(target);
            }

            Destroy(gameObject);
        }

        private void damage(Transform attack) {
            var target = attack.GetComponent<AttackBehaviour>();
            if (target != null) {
                target.TakeDamage(Damage);
            }
        }

        private void explode() {
            Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
            foreach (Collider collider in colliders) {
                if (collider.CompareTag("Attack")) {
                    damage(collider.transform);
                }
            }
        }

    }

}
