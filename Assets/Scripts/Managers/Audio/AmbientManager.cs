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
                FadeManager.I.Fade(
                    Constants.SCENE_TRANSITION_TIME,
                    0f,
                    setSourceVolume,
                    () => {
                        Source.clip = MenuAmbient;
                        Source.Play();
                    },
                    FadeCurve,
                    true);
            } else {
                Source.clip = MenuAmbient;
                Source.Play();
                FadeManager.I.Fade(0f, Constants.SCENE_TRANSITION_TIME, setSourceVolume, null, FadeCurve);
            }
        }

        private void PlayGameAmbient() {
            if (Source.clip && Source.clip.Equals(MenuAmbient)) {
                FadeManager.I.Fade(
                    Constants.SCENE_TRANSITION_TIME,
                    0f,
                    setSourceVolume,
                    () => {
                        Source.clip = GameAmbient;
                        Source.Play();
                    },
                    FadeCurve,
                    true);
            } else {
                Source.clip = GameAmbient;
                Source.Play();
                FadeManager.I.Fade(0f, Constants.SCENE_TRANSITION_TIME, setSourceVolume, null, FadeCurve);
            }
        }

        private void setSourceVolume(float value) {
            Source.volume = value*State.I.Options.AmbientVolume;
        }

    }

}
