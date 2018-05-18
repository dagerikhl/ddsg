using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DdSG {

    public class WaveSpawner: MonoBehaviour {

        [Header("Attributes")]
        public int TotalWaves = 10;
        public float TimeBetweenWaves = 2f;

        public float SpawnRate = 0.5f;

        public int PossibleAttackPatternsPerWave = 2;

        public int MinAttacksPerWave = 2;
        public int MaxAttacksPerWave = 5;
        public int ExtraPotentialAttacksPerWave = 2;

        [Header("Unity Setup Fields")]
        public GameObject AttackPrefab;
        public TextMeshProUGUI WaveCounterText;
        public TextMeshProUGUI WaveCurrentTimeText;
        public TextMeshProUGUI WaveCountdownText;

        // Public members hidden from Unity Inspector
        //[HideInInspector]
        public static int AttacksAlive;

        // Private and protected members
        private int waveIndex;
        private int WaveIndex {
            get { return waveIndex; }
            set {
                waveIndex = value;
                WaveCounterText.text = Formatter.WaveCounterFormat(value).Monospaced();

                PlayerStats.I.Waves++;
            }
        }

        private float currentTime;
        private float CurrentTime {
            get { return currentTime; }
            set {
                currentTime = value;
                WaveCurrentTimeText.text = Formatter.CounterFormat(value).Monospaced();
            }
        }

        private float countdown;
        private float Countdown {
            get { return countdown; }
            set {
                countdown = Mathf.Clamp(value, 0f, Mathf.Infinity);
                WaveCountdownText.text = Formatter.CounterFormat(value).Monospaced();
            }
        }

        private Wave currentWave;
        private Wave nextWave;

        private void Awake() {
            Countdown = TimeBetweenWaves;

            nextWave = pickNewWave();
        }

        private void Update() {
            // Final wave has been defeated
            if (WaveIndex > TotalWaves) {
                GameManager.Win();
                enabled = false;
                return;
            }

            // Wave is still in progress
            if (AttacksAlive > 0) {
                CurrentTime += Time.deltaTime;
                return;
            }

            // We are between waves
            Countdown -= Time.deltaTime;

            // It's time to start the next wave
            if (Countdown <= 0f) {
                CurrentTime = 0f;
                Countdown = TimeBetweenWaves;
                StartCoroutine(spawnWave());
            }
        }

        private IEnumerator spawnWave() {
            WaveIndex++;

            currentWave = nextWave;
            nextWave = pickNewWave();

            AttacksAlive = nextWave.Count;

            for (int i = 0; i < nextWave.Count; i++) {
                spawnAttack(nextWave.AttackPatterns.TakeRandomByLikelihood());
                yield return new WaitForSeconds(1f/SpawnRate);
            }

            foreach (var attackPattern in nextWave.AttackPatterns) {
                spawnAttack(attackPattern);
                yield return new WaitForSeconds(1f/SpawnRate);
            }
        }

        private void spawnAttack(AttackPattern attackPattern) {
            var attack = UnityHelper.Instantiate(AttackPrefab).GetComponent<AttackBehaviour>();
            attack.Initialize(attackPattern);
        }

        private Wave pickNewWave() {
            return new Wave {
                Count = Rnd.Gen.Next(MinAttacksPerWave, MaxAttacksPerWave + 1) + WaveIndex*ExtraPotentialAttacksPerWave,
                AttackPatterns = State.I.GameEntities.SDOs.attack_patterns.TakeRandoms(PossibleAttackPatternsPerWave)
                                      .ToArray()
            };
        }

    }

}
