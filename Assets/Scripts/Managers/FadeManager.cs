using System;
using System.Collections;
using UnityEngine;

namespace DdSG {

    public class FadeManager: SingletonBehaviour<FadeManager> {

        protected FadeManager() {
        }

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private readonly AnimationCurve defaultCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        public void Fade(
            float from,
            float to,
            Action<float> updater,
            Action action = null,
            AnimationCurve curve = null,
            bool fadeBackIn = false) {
            StartCoroutine(fadeRoutine(from, to, updater, action, curve, fadeBackIn));
        }

        private IEnumerator fadeRoutine(
            float from,
            float to,
            Action<float> updater,
            Action action,
            AnimationCurve curve,
            bool fadeBackIn) {
            var direction = to > from ? 1 : -1;

            float t = from;
            while (direction == 1 ? t < to : t > to) {
                t += direction*Time.unscaledDeltaTime;
                float value = (curve ?? defaultCurve).Evaluate(t/(direction == 1 ? to : from));
                updater(value);

                yield return 0;
            }

            if (action != null) {
                action();
            }

            if (fadeBackIn) {
                Fade(to, from, updater);
            }
        }

    }

}
