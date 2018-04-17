using UnityEngine;
using UnityEngine.UI;
using UnitySceneManagement = UnityEngine.SceneManagement;
using System.Collections;

namespace DdSG {

    public class SceneManager: MonoBehaviour {

        [Header("Attributes")]
        public AnimationCurve FadeCurve;

        [Header("Unity Setup Fields")]
        public Image FadeImage;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector

        // Private members

        private void Start() {
            StartCoroutine(fadeIn());
        }

        public void GoTo(string sceneName) {
            // Update last and current scene in state
            State.LastScene = UnitySceneManagement.SceneManager.GetActiveScene().name;
            State.CurrentScene = sceneName;

            // Fade to new scene
            StartCoroutine(fadeOut(sceneName));
        }

        public void GoToLastMenu() {
            GoTo(State.LastScene);
        }

        private IEnumerator fadeIn() {
            float t = 1f;

            while (t > 0f) {
                t -= Time.deltaTime;
                float alpha = FadeCurve.Evaluate(t);
                FadeImage.color = new Color(1f, 1f, 1f, alpha);

                yield return 0;
            }
        }

        private IEnumerator fadeOut(string sceneName) {
            float t = 0f;

            while (t < 1f) {
                t += Time.deltaTime;
                float alpha = FadeCurve.Evaluate(t);
                FadeImage.color = new Color(1f, 1f, 1f, alpha);

                yield return 0;
            }

            UnitySceneManagement.SceneManager.LoadScene(sceneName);
        }

    }

}
