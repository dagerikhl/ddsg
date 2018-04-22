using UnityEngine;

namespace DdSG {

    public abstract class ScenePersistentSingleton<T>: Singleton<T> where T : MonoBehaviour {

        protected abstract string persistentTag { get; }

        private void Awake() {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(persistentTag);

            if (gameObjects.Length > 1) {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

    }

}
