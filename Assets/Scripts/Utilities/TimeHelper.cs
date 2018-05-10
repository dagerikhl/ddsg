using UnityEngine;

namespace DdSG {

    public static class TimeHelper {

        public static string FormatTime(float seconds) {
            var minutes = Mathf.RoundToInt(seconds/60);
            var secondsRemaining = Mathf.RoundToInt(seconds%60);
            Logger.Debug(minutes);
            Logger.Debug(secondsRemaining);
            Logger.Debug(seconds);

            return string.Format("{0}:{1}", minutes, secondsRemaining);
        }

    }

}
