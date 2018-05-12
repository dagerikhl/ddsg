using System.Collections;
using UnityEngine;

namespace DdSG {

    public class AmbientManager: AudioManagerBase<AmbientManager> {

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

        // Private and protected members
        protected override string persistentTag { get { return "AmbientManager"; } }

        private void Start() {
            if (State.I.Options.AmbientEnabled) {
                PlayMenuAmbient();
            }
        }

        public void UpdateAmbient() {
            if (State.I.Options.AmbientEnabled) {
                if (State.I.CurrentScene == Constants.GAME_VIEW) {
                    PlayGameAmbient();
                } else if (State.I.LastScene == Constants.GAME_VIEW) {
                    PlayMenuAmbient();
                }
            }
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

        private void PlayGameAmbient() {
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
                var volume = FadeCurve.Evaluate(t/Constants.SCENE_TRANSITION_TIME)*State.I.Options.AmbientVolume;
                Source.volume = volume;

                yield return 0;
            }
        }

        private IEnumerator fadeOutAndInWithNewAmbient(AudioClip ambient) {
            var t = Constants.SCENE_TRANSITION_TIME;

            while (t > 0f) {
                t -= Time.deltaTime;
                var volume = FadeCurve.Evaluate(t/Constants.SCENE_TRANSITION_TIME)*State.I.Options.AmbientVolume;
                Source.volume = volume;

                yield return 0;
            }

            Source.clip = ambient;
            Source.Play();
            StartCoroutine(fadeIn());
        }

    }

}
