using UnityEngine;

namespace DdSG {

    public class SoundsManager: AudioManager<SoundsManager> {

        protected SoundsManager() {
        }

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public AudioClip ClickSound;
        public AudioClip MildClickSound;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members
        protected override string persistentTag { get { return "SoundsManager"; } }

        public void PlayClickSound() {
            PlaySound(ClickSound);
        }

        public void PlayMildClickSound() {
            PlaySound(MildClickSound);
        }

        private void PlaySound(AudioClip sound) {
            if (AudioEnabled) {
                Source.PlayOneShot(sound);
            }
        }

    }

}
