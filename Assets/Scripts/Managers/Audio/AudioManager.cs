using UnityEngine;

namespace DdSG {

    public abstract class AudioManager<T>: ScenePersistentSingleton<T> where T : MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public AudioSource Source;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members

        public void ToggleEnabled(bool isEnabled) {
            Source.mute = isEnabled;
        }

        public void SetVolume(float volume) {
            Source.volume = volume;
        }

    }

}
