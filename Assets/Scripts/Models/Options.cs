using System;

namespace DdSG {

    [Serializable]
    public class Options {

        public bool AmbientEnabled { get; set; }
        public float AmbientVolume { get; set; }
        public bool SoundsEnabled { get; set; }
        public float SoundsVolume { get; set; }
        public int QualityLevelValue { get; set; }

    }

}
