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
        private IntVector2 targetGridPosition { get { return targetArea.WorldToGrid(targetPosition, SizeOffset); } }

        private Vector3 velocity;

        private void Update() {
            // Check collisions with placement areas
            var mouseRay = HelperObjects.CameraComponent.ScreenPointToRay(Input.mousePosition);
            RaycastHit areaHit;
            Physics.Raycast(mouseRay, out areaHit);

            // Clear all potential tiles when leaving area
            if (targetArea == null && !firstPlacement) {
                clearAllOldTiles();
            }

            if (areaHit.collider != null) {
                targetArea = areaHit.collider.GetComponent<IPlacementArea>();

                if (targetArea != null) {
                    // If first placement, instantly move and enable
                    if (firstPlacement) {
                        firstPlacement = false;
                        transform.position = areaHit.point;
                        transform.localScale = Vector3.one;
                        return;
                    }

                    var snappedPosition = targetArea.Snap(areaHit.point, SizeOffset);
                    var snappedGridPosition = targetArea.WorldToGrid(areaHit.point, SizeOffset);

                    // Check if the ghost fits and isn't too close to the path
                    var fits = targetArea.Fits(snappedGridPosition, SizeOffset) == TowerFitStatus.Fits;
                    Collider[] collisions = Physics.OverlapSphere(areaHit.point, PathCollisionRadius);
                    var collidesWithPath = collisions.Any((c) => c.gameObject.layer == Constants.PATH_LAYER);
                    if (fits && !collidesWithPath) {
                        transform.localScale = Vector3.one;

                        // Set new position and update placement tile state
                        if (snappedPosition != targetPosition) {
                            clearOldTiles();
                            targetArea.Occupy(snappedGridPosition, SizeOffset, PlacementTileState.Potential);

                            targetPosition = snappedPosition;
                        }
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
                clearAllOldTiles();
                enabled = false;
                DestroyThis();
            }
        }

        private void clearAllOldTiles() {
            PlacementTile[] tiles = FindObjectsOfType<PlacementTile>();
            foreach (var tile in tiles) {
                if (tile.State == PlacementTileState.Potential) {
                    tile.SetState(PlacementTileState.Empty);
                }
            }
            transform.localScale = Vector3.zero;
            firstPlacement = true;
        }

        private void clearOldTiles() {
            var oldFits = targetArea.Fits(targetGridPosition, SizeOffset) == TowerFitStatus.Fits;
            if (oldFits) {
                targetArea.Clear(targetGridPosition, SizeOffset);
            }
        }

        private void DestroyThis() {
            GameManager.IsBuilding = false;
            Destroy(gameObject);
        }

    }

}
