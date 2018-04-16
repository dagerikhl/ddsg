using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class OptionsMenu: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public SceneManager SceneManager;
        public Slider VolumeSlider;
        public Toggle MusicToggle;
        public Toggle SoundsToggle;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members

        public void SetVolume(Slider volumeSlider) {
            // Set volume
            AudioListener.volume = volumeSlider.value/10f;

            // Update UI
            if (volumeSlider.value <= 0) {
                MusicToggle.interactable = false;
                SoundsToggle.interactable = false;
            } else {
                MusicToggle.interactable = true;
                SoundsToggle.interactable = true;
            }
        }

        public void ToggleMusic(Toggle musicToggle) {
            // Set volume
            // TODO Distinguish between music and sounds
            AudioListener.volume = 0;

            // Update UI
            if (!musicToggle.isOn && !SoundsToggle.isOn) {
                VolumeSlider.interactable = false;
            } else {
                VolumeSlider.interactable = true;
            }
        }

        public void ToggleSounds(Toggle soundsToggle) {
            // Set volume
            // TODO Distinguish between music and sounds
            AudioListener.volume = 0;

            // Update UI
            if (!soundsToggle.isOn && !MusicToggle.isOn) {
                VolumeSlider.interactable = false;
            } else {
                VolumeSlider.interactable = true;
            }
        }

        public void GoToLastMenu() {
            SceneManager.GoTo(State.LastScene);
        }

    }

}
