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
        private Vector3 pivot;
        private readonly Vector3 offset = new Vector3(50f, 50f);

        private void Awake() {
            var overlayRect = Overlay.GetComponent<RectTransform>().rect;
            pivot = new Vector3(overlayRect.width/2f, overlayRect.height/2f);
        }

        public void Show() {
            FadeManager.I.Fade(0f, Constants.HOVER_TRANSITION_TIME, setAlpha);
        }

        public void Hide() {
            FadeManager.I.Fade(Constants.HOVER_TRANSITION_TIME, 0f, setAlpha, () => Destroy(gameObject));
        }

        public void SetPosition(Vector3 position, bool showUnder, bool showOnLeft) {
            var scale = transform.localScale;
            var direction = new Vector3(showOnLeft ? -1 : 1, showUnder ? -1 : 1);

            Overlay.transform.position = position + Vector3.Scale(Vector3.Scale(pivot + offset, scale), direction);
        }

        public void SetTitle(string title) {
            Title.text = title;
        }

        public void SetText(string text) {
            Text.text = text;
        }

        private void setAlpha(float value) {
            CanvasGroup.alpha = value;
        }

    }

}
