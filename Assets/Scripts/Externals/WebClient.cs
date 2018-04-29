using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace DdSG {

    public class WebClient: MonoBehaviour {

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private FileClient fc;

        private void Start() {
            fc = GetComponent<FileClient>();
        }

        private IEnumerator DownloadJsonToFile<T>(string uri, string filename) {
            var req = UnityWebRequest.Get(uri);
            yield return req.SendWebRequest();

            if (req.isNetworkError || req.isHttpError) {
                Debug.Log(req.error);
            } else {
                var text = req.downloadHandler.text;
                var data = JsonUtility.FromJson<T>(text);
                fc.SaveToFile<T>(filename, data);
            }
        }

    }

}
