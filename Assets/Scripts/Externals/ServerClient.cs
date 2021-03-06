﻿using System.Collections;
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
        public bool HasHadError {
            get {
                if (hasHadError) {
                    hasHadError = false;
                    return true;
                }
                return false;
            }
            private set { hasHadError = value; }
        }

        // Private and protected members
        private string entitiesEndpoint;

        private bool hasHadError;

        private void Awake() {
            entitiesEndpoint = (Debug.isDebugBuild ? Constants.API_URL_DEVELOPMENT : Constants.API_URL)
                               + "/"
                               + "entities";
        }

        public IEnumerator DownloadEntities() {
            yield return StartCoroutine(
                DownloadJsonToFile<EntitiesJson>(entitiesEndpoint, Constants.ENTITIES_FILENAME));
        }

        private IEnumerator DownloadJsonToFile<T>(string uri, string filename) {
            var req = UnityWebRequest.Get(uri);
            yield return req.SendWebRequest();

            if (req.isNetworkError || req.isHttpError) {
                HasHadError = true;
                Logger.Error(req.error);
            } else {
                var text = req.downloadHandler.text;
                var data = JsonConvert.DeserializeObject<T>(text);
                FileClient.I.SaveToFile(filename, data);
            }
        }

    }

}
