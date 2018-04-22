using System.Collections;
using UnityEngine;

namespace DdSG {

    public class AmbientManager: ScenePersistentSingleton<AmbientManager> {

        protected AmbientManager() {
        }

        [Header("Attributes")]
        public AnimationCurve FadeCurve;

        [Header("Unity Setup Fields")]
        public AudioSource AmbientSource;

        public AudioClip MenuAmbient;
        public AudioClip GameAmbient;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members
        protected override string persistentTag { get { return "AmbientMusic"; } }

        private void Start() {
            PlayMenuAmbient();
        }

        public void PlayMenuAmbient() {
            if (AmbientSource.clip && AmbientSource.clip.Equals(GameAmbient)) {
                StartCoroutine(fadeWithAmbient(MenuAmbient));
            } else {
                AmbientSource.clip = MenuAmbient;
                AmbientSource.Play();
                StartCoroutine(fadeIn());
            }
        }

        public void PlayGameAmbient() {
            if (AmbientSource.clip && AmbientSource.clip.Equals(MenuAmbient)) {
                StartCoroutine(fadeWithAmbient(GameAmbient));
            } else {
                AmbientSource.clip = GameAmbient;
                AmbientSource.Play();
                StartCoroutine(fadeIn());
            }
        }

        private IEnumerator fadeIn() {
            var t = 0f;

            while (t < Constants.SCENE_TRANSITION_DURATION) {
                t += Time.deltaTime;
                var volume = FadeCurve.Evaluate(t/Constants.SCENE_TRANSITION_DURATION);
                AmbientSource.volume = volume;

                yield return 0;
            }
        }

        private IEnumerator fadeWithAmbient(AudioClip ambient) {
            var t = Constants.SCENE_TRANSITION_DURATION;

            while (t > 0f) {
                t -= Time.deltaTime;
                var volume = FadeCurve.Evaluate(t/Constants.SCENE_TRANSITION_DURATION);
                AmbientSource.volume = volume;

                yield return 0;
            }

            AmbientSource.clip = ambient;
            AmbientSource.Play();
            StartCoroutine(fadeIn());
        }

    }

}
