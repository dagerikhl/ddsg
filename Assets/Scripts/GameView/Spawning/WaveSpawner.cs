using System.Collections;
using System.Linq;
using UnityEngine;

namespace DdSG {

    public class WaveSpawner: MonoBehaviour {

        [Header("Attributes")]
        public int TotalWaves = 10;
        public float TimeBetweenWaves = 10f;

        [Header("Unity Setup Fields")]
        public GameObject AttackPrefab;

        // Public members hidden from Unity Inspector
        //[HideInInspector]
        public static int AttacksAlive;

        // Private and protected members
        private float countdown = 10f;
        private int waveIndex;

        private void Update() {
            if (AttacksAlive > 0) {
                return;
            }

            if (waveIndex > TotalWaves) {
                GameManager.Win();
                enabled = false;
                return;
            }

            if (countdown <= 0f) {
                StartCoroutine(spawnWave());
                countdown = TimeBetweenWaves;
                return;
            }

            countdown -= Time.deltaTime;
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

            // TODO Set text of some UI component showing countdown to new wave
            // WaveCountdownText.text = string.Format("{0:00.00}", countdown);
        }

        private IEnumerator spawnWave() {
            var wave = new Wave { Count = 5, Rate = 5f };
            wave.AttackPatterns = State.I.GameEntities.SDOs.attack_patterns.TakeRandoms(wave.Count).ToArray();

            AttacksAlive = wave.Count;

            foreach (var attackPattern in wave.AttackPatterns) {
                spawnAttack(attackPattern);
                yield return new WaitForSeconds(1f/wave.Rate);
            }

            PlayerStats.I.Waves++;
            waveIndex++;
        }

        private void spawnAttack(AttackPattern attackPattern) {
            var spawnPoint = SpawnPoints.GetSpawnPoint(attackPattern.custom.injection_vector.categories.TakeRandom());

            var attack = UnityHelper.Instantiate(AttackPrefab, spawnPoint.position, spawnPoint.rotation)
                                    .GetComponent<AttackBehaviour>();
            attack.Initialize(attackPattern);
        }

    }

}
