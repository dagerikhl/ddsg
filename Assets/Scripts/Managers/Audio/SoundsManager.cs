using UnityEngine;

namespace DdSG {

    public class SoundsManager: ScenePersistentSingleton<SoundsManager> {

        protected SoundsManager() {
        }

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public AudioSource SoundsSource;
        public AudioClip ClickSound;
        public AudioClip KillSound;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members
        protected override string persistentTag { get { return "SoundsManager"; } }

        public void PlaySound(AudioClip sound) {
            SoundsSource.PlayOneShot(sound);
        }

        public void PlayClickSound() {
            Debug.Log("Playing");
            SoundsSource.PlayOneShot(ClickSound);
        }

        public void PlayKillSound() {
            SoundsSource.PlayOneShot(KillSound);
        }

    }

}
