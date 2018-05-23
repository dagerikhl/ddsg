using System;
using System.Linq;
using UnityEngine;

namespace DdSG {

    public class MitigationBehaviour: MonoBehaviour {

        [Header("Attributes")]
        public DamageAttribute Damage = new DamageAttribute { MinimumDamage = 5f, MaximumDamage = 15f };
        public float Range = 15f;
        public float FireRate = 1f;
        public float TurnSpeed = 10f;

        [Header("Special Attacks (optional)")]
        public float SlowFactor;

        [Header("Unity Setup Fields")]
        public ClickableBehaviour ClickableBehaviour;

        public Transform Rotatable;
        public Transform FirePoint;
        public GameObject BulletPrefab;
        public Transform RangeIndicator;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]
        public bool IsSelected { set { RangeIndicator.gameObject.SetActive(value); } }

        // Private and protected members
        private CourseOfAction courseOfAction;

        private IPlacementArea placementArea;
        private IntVector2 areaGridPosition;
        private IntVector2 areaSizeOffset;

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

        public void Initialize(
            IPlacementArea area,
            IntVector2 gridPosition,
            IntVector2 sizeOffset,
            CourseOfAction courseOfAction) {
            this.courseOfAction = courseOfAction;

            placementArea = area;
            areaGridPosition = gridPosition;
            areaSizeOffset = sizeOffset;

            Damage = courseOfAction.GetDamage();
            Range = courseOfAction.GetRange();
            RangeIndicator.localScale = Vector3.one*Range;
            FireRate = courseOfAction.GetFireRate();

            // Hover and click actions
            ClickableBehaviour.Title = courseOfAction.custom.mitigation;
            ClickableBehaviour.Text = Formatter.BuildStixDataEntityDescription(courseOfAction);

            ClickableBehaviour.ActionText = "select";
            ClickableBehaviour.PrimaryAction = () => {
                SelectionHelper.DeselectAllMitigations();
                IsSelected = true;

                var title = courseOfAction.custom.mitigation;
                var description = Formatter.BuildStixDataEntityDescription(courseOfAction, true, false);
                var selectedActions = new SelectedAction[] {
                    new SelectedAction(ActionType.Sell, sell),
                    new SelectedAction(
                        ActionType.OpenExternalReferences,
                        () => ReferencesHelper.OpenExternalReferences(courseOfAction))
                };
                HelperObjects.SelectedInfoBar.SelectEntity(title, "Mitigation", description, selectedActions);
            };
            ClickableBehaviour.HasSecondaryAction =
                ReferencesHelper.AddReferencesAsAction(courseOfAction, ClickableBehaviour);
        }

        private void updateTarget() {
            GameObject[] attacks = GameObject.FindGameObjectsWithTag(Constants.ATTACK_TAG);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (GameObject attack in attacks) {
                var attackBehaviour = attack.GetComponent<AttackBehaviour>();

                // Skip all attacks that aren't mitigated
                if (!attackBehaviour.AttackPattern.MitigatedByCategories.Contains(courseOfAction.custom.category)) {
                    continue;
                }

                // Skip invulnerable attacks
                if (attackBehaviour.Invulnerable) {
                    continue;
                }

                float distanceToEnemy = Vector3.Distance(transform.position, attack.transform.position);

                if (distanceToEnemy < shortestDistance) {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = attack;
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
                bullet.Damage = MathHelper.Rangify(
                    Convert.ToSingle(Rnd.Gen.NextDouble()),
                    Damage.MinimumDamage,
                    Damage.MaximumDamage);
                bullet.Seek(target);
            }
        }

        private void sell() {
            PlayerStats.I.Worth += Mathf.CeilToInt(courseOfAction.GetValue()*Constants.SELL_PERCENTAGE);

            placementArea.Clear(areaGridPosition, areaSizeOffset);

            HelperObjects.SelectedInfoBar.Deselect();

            // Spawn effect
            GameObject effect = UnityHelper.Instantiate(BuildManager.I.SellEffect, transform.position);
            Destroy(effect, 5f);

            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Range);
        }

    }

}
