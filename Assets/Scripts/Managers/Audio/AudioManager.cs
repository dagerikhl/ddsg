using UnityEngine;

namespace DdSG {

    public abstract class AudioManager<T>: ScenePersistentSingleton<T> where T : MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public AudioSource Source;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        protected bool AudioEnabled { get; private set; }

        public void EnableAudio() {
            AudioEnabled = true;
            Source.mute = false;
        }

        public void DisableAudio() {
            AudioEnabled = false;
            Source.mute = true;
        }

        public void SetVolume(float volume) {
            Source.volume = volume;
        }

    }

}
