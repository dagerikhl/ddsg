using System.Collections;
using System.Collections.Generic;
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
        public TextMeshProUGUI WaveCounterText;
        public TextMeshProUGUI AttacksAliveText;
        public TextMeshProUGUI WaveCurrentTimeText;
        public TextMeshProUGUI WaveCountdownText;

        public Transform CurrentWaveInfoContainer;
        public Transform NextWaveInfoContainer;

        [Header("Prefabs")]
        public GameObject AttackPrefab;
        public GameObject WaveInformationIconPrefab;

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
        private Wave NextWave {
            get { return nextWave; }
            set {
                if (nextWave != null) {
                    // Clear old icons
                    CurrentWaveInfoContainer.Clear();

                    // Transfer next wave icons to this container
                    NextWaveInfoContainer.StealChildren(CurrentWaveInfoContainer);
                }

                currentWave = nextWave;

                // Generate new icons for this container
                foreach (var attackPattern in value.AttackPatterns) {
                    var icon = Instantiate(WaveInformationIconPrefab, NextWaveInfoContainer)
                        .GetComponent<WaveInfoIcon>();
                    icon.Initialize(attackPattern);
                }

                // Update field
                nextWave = value;
            }
        }

        private void Awake() {
            Countdown = TimeBetweenWaves;

            AttacksAlive = 0;

            NextWave = generateNewWave();
        }

        private void Update() {
            // Update attacks alive
            if (currentWave != null) {
                AttacksAliveText.text = string.Format("{0}/{1}", AttacksAlive, currentWave.Count).Monospaced();
            }

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
            if (Countdown <= 0f) {
                Countdown = TimeBetweenWaves;
            }
            CurrentTime = 0f;
            Countdown -= Time.deltaTime;

            // It's time to start the next wave
            if (Countdown <= 0f) {
                WaveIndex++;
                NextWave = generateNewWave();
                AttacksAlive = currentWave.Count;
                StartCoroutine(spawnWave());
            }
        }

        private IEnumerator spawnWave() {
            for (int i = 0; i < currentWave.Count; i++) {
                spawnAttack(currentWave.AttackPatterns.TakeRandomByLikelihood());
                yield return new WaitForSeconds(1f/SpawnRate);
            }
        }

        private void spawnAttack(AttackPattern attackPattern) {
            var attack = UnityHelper.Instantiate(AttackPrefab).GetComponent<AttackBehaviour>();
            attack.Initialize(attackPattern);
        }

        private Wave generateNewWave() {
            var possibleAttackPatterns = new List<AttackPattern>(PossibleAttackPatternsPerWave);
            while (possibleAttackPatterns.Count < PossibleAttackPatternsPerWave) {
                var possibleAttackPattern = State.I.GameEntities.SDOs.attack_patterns.TakeRandom();
                possibleAttackPatterns.Add(possibleAttackPattern);
                // Ensure that no duplicate attack patterns are chosen for this wave
                possibleAttackPatterns =
                    possibleAttackPatterns.Distinct().DistinctBy((aP) => aP.name.ToLower()).ToList();
            }

            return new Wave {
                Count = Rnd.Gen.Next(MinAttacksPerWave, MaxAttacksPerWave + 1) + WaveIndex*ExtraPotentialAttacksPerWave,
                AttackPatterns = possibleAttackPatterns.ToArray()
            };
        }

    }

}
