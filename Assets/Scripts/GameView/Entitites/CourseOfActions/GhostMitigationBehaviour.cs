using System.Linq;
using UnityEngine;

namespace DdSG {

    public class GhostMitigationBehaviour: MonoBehaviour {

        [Header("Attributes")]
        public float Dampening = 0.075f;
        public IntVector2 SizeOffset = new IntVector2(2, 2);
        public float PathCollisionRadius = 1.5f;

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private bool firstPlacement = true;

        private IPlacementArea targetArea;
        private Vector3 targetPosition;
        private Vector3 velocity;

        private void Update() {
            // Check collisions with placement areas
            var mouseRay = HelperObjects.CameraComponent.ScreenPointToRay(Input.mousePosition);
            RaycastHit areaHit;
            Physics.Raycast(mouseRay, out areaHit);

            if (areaHit.collider != null) {
                targetArea = areaHit.collider.GetComponent<IPlacementArea>();
                if (targetArea != null) {
                    if (firstPlacement) {
                        firstPlacement = false;
                        transform.position = areaHit.point;
                        transform.localScale = Vector3.one;

                        return;
                    }

                    // Check if the ghost is too close to the path
                    Collider[] collisions = Physics.OverlapSphere(areaHit.point, PathCollisionRadius);
                    if (!collisions.Any((c) => c.gameObject.layer == Constants.PATH_LAYER)) {
                        targetPosition = targetArea.Snap(areaHit.point, SizeOffset);
                    }
                }
            }

            // Move ghost
            if (Vector3.SqrMagnitude(transform.position - targetPosition) > 0.01f) {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, Dampening);
            } else {
                velocity = Vector3.zero;
            }

            // Build when clicked
            if (Input.GetMouseButtonDown(0)) {
                if (targetArea != null && !GameManager.IsUiBlocking) {
                    BuildManager.I.ImplementMitigation(
                        targetArea,
                        targetArea.WorldToGrid(targetPosition, SizeOffset),
                        SizeOffset);

                    DestroyThis();
                }
            } else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape)) {
                DestroyThis();
            }
        }

        private void DestroyThis() {
            GameManager.IsBuilding = false;
            Destroy(gameObject);
        }

    }

}
