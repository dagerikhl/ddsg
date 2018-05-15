using UnityEngine;

namespace DdSG {

    public static class CameraHelper {

        public static Quaternion GetCameraRotationXy() {
            var cameraRotation = HelperObjects.Camera.rotation.eulerAngles;
            var rotation = new Vector3(cameraRotation.x, cameraRotation.y, 0f);
            return Quaternion.Euler(rotation);
        }

    }

}
