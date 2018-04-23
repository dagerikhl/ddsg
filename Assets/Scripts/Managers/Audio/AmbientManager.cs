using System.Collections;
using UnityEngine;

namespace DdSG {

    public class AmbientManager: AudioManager<AmbientManager> {

        protected AmbientManager() {
        }

        [Header("Attributes")]
        public AnimationCurve FadeCurve;

        [Header("Unity Setup Fields")]
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
            if (Source.clip && Source.clip.Equals(GameAmbient)) {
                StartCoroutine(fadeOutAndInWithNewAmbient(MenuAmbient));
            } else {
                Source.clip = MenuAmbient;
                Source.Play();
                StartCoroutine(fadeIn());
            }
        }

        public void PlayGameAmbient() {
            if (Source.clip && Source.clip.Equals(MenuAmbient)) {
                StartCoroutine(fadeOutAndInWithNewAmbient(GameAmbient));
            } else {
                Source.clip = GameAmbient;
                Source.Play();
                StartCoroutine(fadeIn());
            }
        }

        private IEnumerator fadeIn() {
            var t = 0f;

            while (t < Constants.SCENE_TRANSITION_TIME) {
                t += Time.deltaTime;
                var volume = FadeCurve.Evaluate(t/Constants.SCENE_TRANSITION_TIME)*State.I.AmbientVolume;
                Source.volume = volume;

                yield return 0;
            }
        }

        private IEnumerator fadeOutAndInWithNewAmbient(AudioClip ambient) {
            var t = Constants.SCENE_TRANSITION_TIME;

            while (t > 0f) {
                t -= Time.deltaTime;
                var volume = FadeCurve.Evaluate(t/Constants.SCENE_TRANSITION_TIME)*State.I.AmbientVolume;
                Source.volume = volume;

                yield return 0;
            }

            Source.clip = ambient;
            Source.Play();
            StartCoroutine(fadeIn());
        }

    }

}
