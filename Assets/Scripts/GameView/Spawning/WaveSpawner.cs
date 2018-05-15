using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class WaveSpawner: MonoBehaviour {

        [Header("Attributes")]
        public float TimeBetweenWaves = 2f;

        [Header("Unity Setup Fields")]
        public Wave[] Waves;
        public Transform SpawnPoint;
        public GameManager GameManager;

        public Text WaveCountdownText;

        // Public members hidden from Unity Inspector
        //[HideInInspector]
        public static int AttacksAlive;

        // Private and protected members
        private float countdown = 2f;
        private int waveIndex;

        private void Update() {
            if (AttacksAlive > 0) {
                return;
            }

            if (waveIndex == Waves.Length) {
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

            WaveCountdownText.text = string.Format("{0:00.00}", countdown);
        }

        private IEnumerator spawnWave() {
            Wave wave = Waves[waveIndex];
            AttacksAlive = wave.Count;

            for (var i = 0; i < wave.Count; i++) {
                spawnAttack(wave.Attack);
                yield return new WaitForSeconds(1f/wave.Rate);
            }

            PlayerStats.I.Waves++;
            waveIndex++;
        }

        private void spawnAttack(GameObject enemy) {
            Instantiate(enemy, SpawnPoint.position, SpawnPoint.rotation, HelperObjects.Ephemerals);
        }

    }

}
