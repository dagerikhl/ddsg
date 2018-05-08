using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace DdSG {

    public class OptionsMenu: MonoBehaviour {

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]
        public Toggle AmbientToggle;
        public Slider AmbientSlider;
        public Toggle SoundsToggle;
        public Slider SoundsSlider;

        public Dropdown QualityDropdown;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        private void Start() {
            // Disable sounds when setting the initial state
            SoundsManager.I.DisableAudio();

            AmbientToggle.isOn = State.I.Options.AmbientEnabled;
            AmbientSlider.value = Mathf.RoundToInt(State.I.Options.AmbientVolume*10f);
            SoundsToggle.isOn = State.I.Options.SoundsEnabled;
            SoundsSlider.value = Mathf.RoundToInt(State.I.Options.SoundsVolume*10f);

            QualityDropdown.value = QualitySettings.GetQualityLevel();

            // Restore sounds after setting the initial state
            SoundsManager.I.EnableAudio();
        }

        [UsedImplicitly]
        public void ToggleAmbient(Toggle toggle) {
            State.I.Options.AmbientEnabled = toggle.isOn;
            if (toggle.isOn) {
                AmbientManager.I.EnableAudio();
            } else {
                AmbientManager.I.DisableAudio();
            }

            AmbientSlider.interactable = toggle.isOn;

            if (toggle.isOn) {
                AmbientManager.I.PlayMenuAmbient();
            }
        }

        [UsedImplicitly]
        public void ChangeAmbientVolume(Slider slider) {
            var volume = slider.value/10f;

            State.I.Options.AmbientVolume = volume;
            AmbientManager.I.SetVolume(volume);
        }

        [UsedImplicitly]
        public void ToggleSounds(Toggle toggle) {
            State.I.Options.SoundsEnabled = toggle.isOn;
            if (toggle.isOn) {
                SoundsManager.I.EnableAudio();
            } else {
                SoundsManager.I.DisableAudio();
            }

            SoundsSlider.interactable = toggle.isOn;
        }

        [UsedImplicitly]
        public void ChangeSoundsVolume(Slider slider) {
            var volume = slider.value/10f;

            State.I.Options.SoundsVolume = volume;
            SoundsManager.I.SetVolume(volume);
        }

        [UsedImplicitly]
        public void ChangeQuality(Dropdown qualityDropdown) {
            QualitySettings.SetQualityLevel(qualityDropdown.value);
        }

    }

}
