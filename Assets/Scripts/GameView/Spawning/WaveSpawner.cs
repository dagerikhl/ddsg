using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DdSG {

    public class WaveSpawner: SingletonBehaviour<WaveSpawner> {

        protected WaveSpawner() {
        }

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
        [HideInInspector]
        private int attacksAlive;
        public int AttacksAlive {
            get { return attacksAlive; }
            set {
                attacksAlive = value;

                if (currentWave != null) {
                    AttacksAliveText.text = Formatter.CountOfMaxFormat(value, currentWave.Count).Monospaced();
                }
            }
        }

        private Wave nextWave;
        public Wave NextWave {
            get { return nextWave; }
            set {
                // Generate new icons for this container
                NextWaveInfoContainer.Clear();
                foreach (var attackPattern in value.AttackPatterns) {
                    var icon = Instantiate(WaveInformationIconPrefab, NextWaveInfoContainer)
                        .GetComponent<WaveInfoIcon>();
                    icon.Initialize(attackPattern);
                }

                // Update field
                nextWave = value;
            }
        }

        // Private and protected members
        private int waveIndex;
        private int WaveIndex {
            get { return waveIndex; }
            set {
                waveIndex = value;
                WaveCounterText.text = Formatter.CountOfMaxFormat(value, TotalWaves).Monospaced();

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

        private void Awake() {
            Countdown = TimeBetweenWaves;

            AttacksAlive = 0;

            NextWave = GenerateNewWave();
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
            if (Countdown <= 0f) {
                Countdown = TimeBetweenWaves;
            }
            CurrentTime = 0f;
            Countdown -= Time.deltaTime;

            // It's time to start the next wave
            if (Countdown <= 0f) {
                WaveIndex++;

                // Update current wave
                if (nextWave != null) {
                    // Clear old icons
                    CurrentWaveInfoContainer.Clear();

                    // Transfer next wave icons to current wave container
                    NextWaveInfoContainer.TransferChildrenTo(CurrentWaveInfoContainer);
                }

                currentWave = nextWave;
                AttacksAlive = currentWave == null ? 0 : currentWave.Count;

                // Set up next wave
                NextWave = GenerateNewWave();
                StartCoroutine(spawnWave());
            }
        }

        public Wave GenerateNewWave() {
            var possibleAttackPatterns = new List<AttackPattern>();
            while (possibleAttackPatterns.Count
                   < Mathf.Min(State.I.GameEntities.SDOs.attack_patterns.Length, PossibleAttackPatternsPerWave)) {
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

    }

}
