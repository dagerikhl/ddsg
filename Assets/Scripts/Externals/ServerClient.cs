using System;

namespace DdSG {

    public class ServerClient {

        private const string apiEndpoint = "/entities";
        private readonly string apiPath;

        public ServerClient() {
            apiPath = Environment.GetEnvironmentVariable("API_URI") + apiEndpoint;
        }

    }

}
