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
        public Dropdown QualityDropdown;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private members

        private void Start() {
            VolumeSlider.value = Mathf.RoundToInt(AudioListener.volume*10f);
            MusicToggle.isOn = !AudioListener.pause;
            SoundsToggle.isOn = !AudioListener.pause;
            QualityDropdown.value = QualitySettings.GetQualityLevel();
        }

        public void ChangeVolume(Slider volumeSlider) {
            // Set volume
            AudioListener.volume = volumeSlider.value/10f;
        }

        public void ToggleMusic(Toggle musicToggle) {
            // Set volume
            // TODO Distinguish between music and sounds
            AudioListener.pause = true;

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
            AudioListener.pause = true;

            // Update UI
            if (!soundsToggle.isOn && !MusicToggle.isOn) {
                VolumeSlider.interactable = false;
            } else {
                VolumeSlider.interactable = true;
            }
        }

        public void ChangeQuality(Dropdown qualityDropdown) {
            QualitySettings.SetQualityLevel(qualityDropdown.value);
        }

    }

}
