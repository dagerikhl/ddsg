using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace DdSG {

    /// <inheritdoc />
    /// <summary>
    /// Be aware this will not prevent a non singleton constructor
    ///   such as `T myT = new T();`
    /// To prevent that, add `protected T () {}` to your singleton class.
    ///
    /// As a note, this is made as MonoBehaviour because we need Coroutines.
    /// </summary>
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public class Singleton<T>: MonoBehaviour where T : MonoBehaviour {

        private static T instance;

        private static readonly object _lock = new object();

        public static T I {
            get {
                lock (_lock) {
                    if (instance == null) {
                        instance = (T) FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1) {
                            Debug.LogError(
                                "[Singleton] Something went really wrong "
                                + " - there should never be more than 1 singleton!"
                                + " Reopening the scene might fix it.");
                            return instance;
                        }

                        if (instance == null) {
                            GameObject singleton = new GameObject();
                            instance = singleton.AddComponent<T>();
                            singleton.name = "(singleton) " + typeof(T);

                            DontDestroyOnLoad(singleton);

                            Debug.Log(
                                "[Singleton] An instance of "
                                + typeof(T)
                                + " is needed in the scene, so '"
                                + singleton
                                + "' was created with DontDestroyOnLoad.");
                        } else {
                            Debug.Log("[Singleton] Using instance already created: " + instance.gameObject.name);
                        }
                    }

                    return instance;
                }
            }
        }

    }

}
