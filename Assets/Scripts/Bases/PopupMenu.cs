using DdSG;
using UnityEngine;

namespace Bases {

    public abstract class PopupMenu<T>: SingletonBehaviour<T> where T : MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public AnimationCurve FadeCurve;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector

        // Private and protected members
        private CanvasGroup canvasGroup;

        private void Awake() {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        protected void show() {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }

        protected void hide() {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
        }

        protected void setAlpha(float value) {
            canvasGroup.alpha = value;
        }

    }

}
