using UnityEngine;

namespace DdSG {

    public class SoundsManager: Singleton<SoundsManager> {

        protected SoundsManager() {
        }

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public AudioSource SoundsSource;
        public AudioClip KillSound;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members

        public void PlaySound(AudioClip sound) {
            SoundsSource.PlayOneShot(sound);
        }

        public void PlayKillSound() {
            SoundsSource.PlayOneShot(KillSound);
        }

    }

}
