using UnityEngine;

namespace DdSG {

    public static class HelperObjects {

        public static Transform Ephemerals { get { return GameObject.FindWithTag("Ephemerals").transform; } }

        public static GameObject Camera { get { return GameObject.FindWithTag("MainCamera"); } }
        public static Transform CameraTransform { get { return Camera.transform; } }
        public static Camera CameraComponent { get { return Camera.GetComponent<Camera>(); } }
        public static CameraManager CameraManager { get { return Camera.GetComponent<CameraManager>(); } }

        public static PauseMenu PauseMenu { get { return Object.FindObjectOfType<PauseMenu>(); } }

        public static SelectedInfoBar SelectedInfoBar {
            get { return GameObject.FindWithTag("SelectedInfoBar").GetComponent<SelectedInfoBar>(); }
        }

        public static GameObject HoverOverlayPrefab { get; set; }
        public static GameObject GhostMitigationPrefab { get; set; }
        public static GameObject EnteredSystemMessagePrefab { get; set; }

    }

}
