using System.Collections;
using TMPro;
using UnityEngine;

namespace DdSG {

    public class EnteredSystemMessage: MonoBehaviour {

        [Header("Attributes")]
        public float Lifetime = 1f;

        [Header("Unity Setup Fields")]
        public TextMeshProUGUI Label;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private CanvasGroup canvasGroup;

        private void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();

            StartCoroutine(animateAndDestroy());
        }

        private IEnumerator animateAndDestroy() {
            var t = Lifetime;

            while (t > 0f) {
                t -= Time.deltaTime;

                canvasGroup.alpha = t/Lifetime;
                transform.position = transform.position.WithNewY(transform.position.y + t/10f);

                yield return 0;
            }

            Destroy(gameObject);

            yield return 0;
        }

    }

}
