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
        public TextMeshProUGUI WaveCurrentTimeText;
        public TextMeshProUGUI WaveCountdownText;

        // Public members hidden from Unity Inspector
        //[HideInInspector]
        public static int AttacksAlive;

        // Private and protected members
        private float currentTime;
        private float countdown = 5f;
        private int waveIndex;

        private void Update() {
            if (AttacksAlive > 0) {
                currentTime += Time.deltaTime;
                WaveCurrentTimeText.text = TimeHelper.CounterFormat(currentTime).Monospaced();
                return;
            }

            if (waveIndex > TotalWaves) {
                GameManager.Win();
                enabled = false;
                return;
            }

            currentTime = 0f;
            WaveCurrentTimeText.text = TimeHelper.CounterFormat(currentTime).Monospaced();
            if (countdown <= 0f) {
                countdown = TimeBetweenWaves;
                StartCoroutine(spawnWave());
                return;
            }

            countdown -= Time.deltaTime;
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            WaveCountdownText.text = TimeHelper.CounterFormat(countdown).Monospaced();
        }

        private IEnumerator spawnWave() {
            var wave = new Wave { Count = 10, Rate = 0.5f };
            // TODO Use likelihood here to determine how often to pick the attack pattern
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
            var attack = UnityHelper.Instantiate(AttackPrefab).GetComponent<AttackBehaviour>();
            attack.Initialize(attackPattern);
        }

    }

}
