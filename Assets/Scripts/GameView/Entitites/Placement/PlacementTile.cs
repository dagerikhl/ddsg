using UnityEngine;

namespace DdSG {

    /// <summary>
    /// Simple class to illustrate tile placement locations
    ///
    /// All credit goes to Unity3d Technologies.
    /// </summary>
    public class PlacementTile: MonoBehaviour {

        public PlacementTileState State;

        /// <summary>
        /// Material to use when this tile is empty
        /// </summary>
        public Material EmptyMaterial;
        /// <summary>
        /// Material to use when this tile is filled
        /// </summary>
        public Material PotentialMaterial;
        /// <summary>
        /// Material to use when this tile can be potentially filled
        /// </summary>
        public Material FilledMaterial;
        /// <summary>
        /// The renderer whose material we're changing
        /// </summary>
        public Renderer TileRenderer;

        /// <summary>
        /// Update the state of this placement tile
        /// </summary>
        public void SetState(PlacementTileState newState) {
            State = newState;

            switch (newState) {
            case PlacementTileState.Filled:
                if (TileRenderer != null && FilledMaterial != null) {
                    TileRenderer.sharedMaterial = FilledMaterial;
                }
                break;
            case PlacementTileState.Potential:
                if (TileRenderer != null && PotentialMaterial != null) {
                    TileRenderer.sharedMaterial = PotentialMaterial;
                }
                break;
            case PlacementTileState.Empty:
                if (TileRenderer != null && EmptyMaterial != null) {
                    TileRenderer.sharedMaterial = EmptyMaterial;
                }
                break;
            }
        }

    }

}
