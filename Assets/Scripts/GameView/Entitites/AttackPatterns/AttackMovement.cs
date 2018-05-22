using System.Linq;
using UnityEngine;

namespace DdSG {

    [RequireComponent(typeof(AttackBehaviour))]
    public class AttackMovement: MonoBehaviour {

        //[Header("Blueprints")]

        [Header("Attributes")]
        public float DistanceToWaypoint = 0.4f;

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private AttackBehaviour attackBehaviour;

        private Waypoints waypoints;

        private Transform target;
        private int waypointIndex;

        private void Start() {
            attackBehaviour = GetComponent<AttackBehaviour>();

            waypoints = FindObjectsOfType<Waypoints>().First((w) => w.Category == attackBehaviour.InjectionVector);

            target = waypoints.Points[0];
        }

        private void Update() {
            if (target != null) {
                Vector3 dir = target.position - transform.position;
                transform.Translate(dir.normalized*attackBehaviour.Speed*Time.deltaTime, Space.World);

                if (Vector3.Distance(transform.position, target.position) <= DistanceToWaypoint) {
                    getNextWaypoint();
                }

                attackBehaviour.Speed = attackBehaviour.StartSpeed;
            }
        }

        private void getNextWaypoint() {
            if (waypointIndex == waypoints.Points.Length - 1) {
                waypointIndex++;
                target = AssetSockets.GetSocketPosition(attackBehaviour.TargetedAssetIndex);
                DistanceToWaypoint = 5f;
                return;
            }
            if (waypointIndex == waypoints.Points.Length) {
                endPath();
                return;
            }

            target = waypoints.Points[++waypointIndex];
        }

        private void endPath() {
            attackBehaviour.DamageAsset();

            // SFX
            var effect = UnityHelper.Instantiate(attackBehaviour.DeathEffect, transform.position);
            Destroy(effect, 5f);

            WaveSpawner.I.AttacksAlive--;
            Destroy(gameObject);
        }

    }

}
