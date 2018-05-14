using System;
using System.Collections;
using UnityEngine;

namespace DdSG {

    public class FadeManager: SingletonBehaviour<FadeManager> {

        protected FadeManager() {
        }

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public AnimationCurve FadeCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        public IEnumerator Fade(float from, float to, Action<float> updater, Action action = null) {
            var direction = to > from ? 1 : -1;

            float t = from;
            while (direction == 1 ? t < to : t > to) {
                t += direction*Time.deltaTime;
                float value = FadeCurve.Evaluate(t/(direction == 1 ? to : from));
                updater(value);

                yield return 0;
            }

            // Perform action if supplied
            if (action != null) {
                action();
            }
        }

    }

}
