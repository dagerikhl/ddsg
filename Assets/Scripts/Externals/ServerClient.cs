using System;
using UnityEngine;

namespace DdSG {

    public class ServerClient: MonoBehaviour {

        //[Header("Attributes")]

        [Header("Unity Setup Fields")]
        public WebClient WebClient;

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private readonly string entitiesEndpoint = Environment.GetEnvironmentVariable("API_URI") + "/" + "entities";

        public void DownloadEntities() {
            StartCoroutine(WebClient.DownloadJsonToFile<Entities>(entitiesEndpoint, "entities.json"));
        }

    }

}
