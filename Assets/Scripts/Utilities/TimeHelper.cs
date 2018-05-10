using System;
using UnityEngine;

namespace DdSG {

    public static class TimeHelper {

        public static string FormatTime(float seconds) {
            var secondsRounded = Mathf.RoundToInt(seconds);
            var time = new TimeSpan(0, 0, secondsRounded);

            return string.Format(
                "{0}{1:D2}:{2:D2}",
                time.Hours == 0 ? " " : "",
                time.Hours*60 + time.Minutes,
                time.Seconds);
        }

    }

}
