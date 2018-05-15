using System;
using UnityEngine;

namespace DdSG {

    public class SpawnPoints: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public Transform NetworkSpawnPoint;
        public Transform ClientSpawnPoint;
        public Transform ServerSpawnPoint;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector

        // Private and protected members
        private static Transform[] spawnPoints;

        private void Awake() {
            spawnPoints = new[] { ClientSpawnPoint, NetworkSpawnPoint, ServerSpawnPoint };
        }

        public static Transform GetSpawnPoint(PathCategory category) {
            switch (category) {
            case PathCategory.Client:
                return spawnPoints[0];
            case PathCategory.Network:
                return spawnPoints[1];
            case PathCategory.Server:
                return spawnPoints[2];
            default:
                throw new ArgumentOutOfRangeException("category", category, "Path category doesn't exist.");
            }
        }

    }

}
