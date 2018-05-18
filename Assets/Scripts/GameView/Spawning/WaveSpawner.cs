using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DdSG {

    public class WaveSpawner: MonoBehaviour {

        [Header("Attributes")]
        public int TotalWaves = 10;
        public float TimeBetweenWaves = 2f;

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

        private void Awake() {
            Countdown = TimeBetweenWaves;
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

            var wave = new Wave { Count = 3, Rate = 0.5f };
            // TODO Use likelihood here to determine how often to pick the attack pattern
            wave.AttackPatterns = State.I.GameEntities.SDOs.attack_patterns.TakeRandoms(wave.Count).ToArray();

            AttacksAlive = wave.Count;

            foreach (var attackPattern in wave.AttackPatterns) {
                spawnAttack(attackPattern);
                yield return new WaitForSeconds(1f/wave.Rate);
            }
        }

        private void spawnAttack(AttackPattern attackPattern) {
            var attack = UnityHelper.Instantiate(AttackPrefab).GetComponent<AttackBehaviour>();
            attack.Initialize(attackPattern);
        }

    }

}
