﻿using UnityEngine;

namespace DdSG {

    public class MitigationBehaviour: MonoBehaviour {

        [Header("Attributes")]
        public float Range = 15f;
        public float FireRate = 1f;
        public float TurnSpeed = 10f;

        [Header("Special Attacks (optional)")]
        public float SlowFactor;

        [Header("Unity Setup Fields")]
        public Transform Rotatable;
        public Transform FirePoint;
        public GameObject BulletPrefab;
        public string AttackTag = "Attack";

        //[Header("Optional")]

        // Public members hidden from Unity Inspector

        // Private and protected members
        private float fireCountdown;

        private Transform target;

        private void Start() {
            InvokeRepeating("updateTarget", 0f, 0.5f);
        }

        private void Update() {
            if (target == null) {
                return;
            }

            lockOnTarget();

            if (fireCountdown <= 0f) {
                shoot();
                fireCountdown = 1f/FireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Range);
        }

        private void updateTarget() {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(AttackTag);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (GameObject enemy in enemies) {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < shortestDistance) {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= Range) {
                target = nearestEnemy.transform;
            } else {
                target = null;
            }
        }

        private void lockOnTarget() {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(Rotatable.rotation, lookRotation, Time.deltaTime*TurnSpeed).eulerAngles;
            Rotatable.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        private void shoot() {
            var bullet = UnityHelper.Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation)
                                    .GetComponent<Bullet>();

            if (bullet != null) {
                bullet.Seek(target);
            }
        }

    }

}