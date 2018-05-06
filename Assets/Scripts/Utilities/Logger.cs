namespace DdSG {

    public static class Logger {

        public static void Debug(object message) {
            if (UnityEngine.Debug.isDebugBuild) {
                UnityEngine.Debug.Log(message);
            }
        }

        public static void Error(object message) {
            if (UnityEngine.Debug.isDebugBuild) {
                UnityEngine.Debug.LogError(message);
            }
        }

    }

}
