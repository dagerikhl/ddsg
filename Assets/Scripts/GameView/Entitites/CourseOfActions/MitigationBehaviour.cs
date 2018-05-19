using UnityEngine;

namespace DdSG {

    public class MitigationBehaviour: MonoBehaviour {

        [Header("Attributes")]
        public float Range = 15f;
        public float FireRate = 1f;
        public float TurnSpeed = 10f;
        public int Value = 10;

        [Header("Special Attacks (optional)")]
        public float SlowFactor;

        [Header("Unity Setup Fields")]
        public Transform Rotatable;
        public Transform FirePoint;
        public GameObject BulletPrefab;
        public string AttackTag = "Attack";

        public HoverBehaviour HoverBehaviour;
        public ActionEvents ActionEvents;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector

        // Private and protected members
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
            placementArea = area;
            areaGridPosition = gridPosition;
            areaSizeOffset = sizeOffset;

            HoverBehaviour.Title = courseOfAction.custom.mitigation;
            HoverBehaviour.Text = Formatter.BuildStixDataEntityDescription(courseOfAction);

            HoverBehaviour.ActionText = "select";
            ActionEvents.PrimaryAction = () => {
                var title = courseOfAction.custom.mitigation;
                var description = Formatter.BuildStixDataEntityDescription(courseOfAction, true, false);
                var selectedActions = new SelectedAction[] { new SelectedAction(ActionType.Sell, sell) };
                HelperObjects.SelectedInfoBar.SelectEntity(title, "Mitigation", description, selectedActions);
            };
            HoverBehaviour.HasSecondaryAction = ReferencesHelper.AddReferencesAsAction(courseOfAction, ActionEvents);
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

        private void sell() {
            PlayerStats.I.Worth += Mathf.CeilToInt(Value*Constants.SELL_PERCENTAGE);

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
