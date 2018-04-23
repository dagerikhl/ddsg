using UnityEngine;

namespace DdSG {

    public class SoundsManager: AudioManager<SoundsManager> {

        protected SoundsManager() {
        }

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public AudioClip ClickSound;
        public AudioClip MildClickSound;
        public AudioClip KillSound;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members
        protected override string persistentTag { get { return "SoundsManager"; } }

        public void PlaySound(AudioClip sound) {
            Source.PlayOneShot(sound);
        }

        public void PlayClickSound() {
            Source.PlayOneShot(ClickSound);
        }

        public void PlayMildClickSound() {
            Source.PlayOneShot(MildClickSound);
        }

        // TODO Impl. functionality where kill sound will be required
        public void PlayKillSound() {
            Source.PlayOneShot(KillSound);
        }

    }

}
