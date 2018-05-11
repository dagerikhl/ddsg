using TMPro;
using UnityEngine;

namespace DdSG {

    public class HoverOverlay: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public GameObject Overlay;
        public TextMeshProUGUI Title;
        public TextMeshProUGUI Text;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private Vector3 pivot;

        private void Awake() {
            var overlayRect = Overlay.GetComponent<RectTransform>().rect;
            pivot = new Vector3(overlayRect.width/2f, overlayRect.height/2f);

            SetActive(false);
        }

        public void SetActive(bool active) {
            Overlay.SetActive(active);
        }

        public void SetPosition(Vector3 position, bool showUnder, bool showOnLeft) {
            var offset = new Vector3(showOnLeft ? -1 : 1, showUnder ? -1 : 1);
            Overlay.transform.position = position + Vector3.Scale(pivot, offset);
        }

        public void SetTitle(string title) {
            Title.text = title;
        }

        public void SetText(string text) {
            Text.text = text;
        }

    }

}
