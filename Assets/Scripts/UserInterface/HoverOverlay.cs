﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DdSG {

    public class HoverOverlay: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public GameObject Overlay;
        public TextMeshProUGUI Title;
        public TextMeshProUGUI Text;
        public CanvasGroup CanvasGroup;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private readonly Vector3 offset = new Vector3(20f, 20f);

        public void Initialize(string title, string text, Vector3 position, bool showUnder, bool showOnLeft) {
            Title.text = title;
            Text.text = text;

            StartCoroutine(
                setPositionRoutine(
                    position,
                    showUnder,
                    showOnLeft,
                    () => FadeManager.I.Fade(0f, Constants.HOVER_TRANSITION_TIME, setAlpha)));
        }

        public void Destroy() {
            FadeManager.I.Fade(Constants.HOVER_TRANSITION_TIME, 0f, setAlpha, () => Destroy(gameObject));
        }

        private IEnumerator setPositionRoutine(Vector3 position, bool showUnder, bool showOnLeft, Action action) {
            // Has to be done because height of Overlay is not updated immediately according to content size fitter
            yield return new WaitForSeconds(0f);

            var overlayRect = Overlay.GetComponent<RectTransform>().rect;
            var origin = new Vector3(overlayRect.width/2f, overlayRect.height/2f);

            var scale = transform.localScale;
            var direction = new Vector3(showOnLeft ? -1 : 1, showUnder ? -1 : 1);

            Overlay.transform.position = position + Vector3.Scale(Vector3.Scale(origin, scale) + offset, direction);

            if (action != null) {
                action();
            }
        }

        private void setAlpha(float value) {
            CanvasGroup.alpha = value;
        }

    }

}
