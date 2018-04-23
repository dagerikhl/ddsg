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

        // Private members

        private void Start() {
            AmbientToggle.isOn = State.I.AmbientEnabled;
            AmbientSlider.value = Mathf.RoundToInt(State.I.AmbientVolume*10f);
            SoundsToggle.isOn = State.I.SoundsEnabled;
            SoundsSlider.value = Mathf.RoundToInt(State.I.SoundsVolume*10f);

            QualityDropdown.value = QualitySettings.GetQualityLevel();
        }

        [UsedImplicitly]
        public void ToggleAmbient(Toggle toggle) {
            State.I.AmbientEnabled = toggle.isOn;
            AmbientManager.I.ToggleEnabled(toggle.isOn);

            AmbientSlider.interactable = toggle.isOn;

            if (toggle.isOn) {
                AmbientManager.I.PlayMenuAmbient();
            }
        }

        [UsedImplicitly]
        public void ChangeAmbientVolume(Slider slider) {
            var volume = slider.value/10f;

            State.I.AmbientVolume = volume;
            AmbientManager.I.SetVolume(volume);
        }

        [UsedImplicitly]
        public void ToggleSounds(Toggle toggle) {
            State.I.SoundsEnabled = toggle.isOn;
            SoundsManager.I.ToggleEnabled(toggle.isOn);

            SoundsSlider.interactable = toggle.isOn;
        }

        [UsedImplicitly]
        public void ChangeSoundsVolume(Slider slider) {
            var volume = slider.value/10f;

            State.I.SoundsVolume = volume;
            SoundsManager.I.SetVolume(volume);
        }

        [UsedImplicitly]
        public void ChangeQuality(Dropdown qualityDropdown) {
            QualitySettings.SetQualityLevel(qualityDropdown.value);
        }

    }

}
