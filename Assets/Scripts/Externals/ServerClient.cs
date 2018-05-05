using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace DdSG {

    public class ServerClient: SingletonBehaviour<ServerClient> {

        protected ServerClient() {
        }

        //[Header("Attributes")]

        //[Header("Unity Setup Fields")]

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private string entitiesEndpoint;

        private void Start() {
            entitiesEndpoint = Constants.API_URL + "/" + "entities";
        }

        public IEnumerator DownloadEntities() {
            yield return StartCoroutine(DownloadJsonToFile<EntitiesJson>(entitiesEndpoint, "entities.json"));
        }

        private IEnumerator DownloadJsonToFile<T>(string uri, string filename) {
            var req = UnityWebRequest.Get(uri);
            yield return req.SendWebRequest();

            if (req.isNetworkError || req.isHttpError) {
                Debug.Log(req.error);
            } else {
                var text = req.downloadHandler.text;
                var data = JsonConvert.DeserializeObject<T>(text);
                FileClient.I.SaveToFile(filename, data);
            }
        }

    }

}
