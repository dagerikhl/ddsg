using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace DdSG {

    public class WebClient: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public FileClient FileClient;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        public IEnumerator DownloadJsonToFile<T>(string uri, string filename) {
            var req = UnityWebRequest.Get(uri);
            yield return req.SendWebRequest();

            if (req.isNetworkError || req.isHttpError) {
                Debug.Log(req.error);
            } else {
                var text = req.downloadHandler.text;
                var data = JsonUtility.FromJson<T>(text);
                FileClient.SaveToFile(filename, data);
            }
        }

    }

}
