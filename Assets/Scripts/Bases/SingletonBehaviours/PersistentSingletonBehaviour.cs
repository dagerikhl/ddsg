using UnityEngine;

namespace DdSG {

    /// <inheritdoc />
    /// <summary>
    /// Makes sure this class stays in the game even through scene changes.
    /// </summary>
    /// <typeparam name="T">Pass along the type of the extended class to the base SingletonBehaviour.</typeparam>
    public abstract class PersistentSingletonBehaviour<T>: SingletonBehaviour<T> where T : MonoBehaviour {

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
