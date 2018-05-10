using System;
using UnityEngine;

namespace DdSG {

    /// <summary>
    /// All credits for this class goes to Denis Sylkin for his RTS Camera:
    /// https://assetstore.unity.com/packages/tools/camera/rts-camera-43321
    /// </summary>
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Camera Manager")]
    public class CameraManager: MonoBehaviour {

        private Vector3 originalPosition;

        private void Awake() {
            originalPosition = LimitMapOriginalPos ? transform.position : Vector3.zero;
        }

        #region Foldouts

#if UNITY_EDITOR
        public int LastTab;
#endif

        #endregion

        private Transform mTransform; //camera tranform
        public bool UseFixedUpdate; //use FixedUpdate() or Update()

        #region Movement

        public float KeyboardMovementSpeed = 5f; //speed with keyboard movement
        public float ScreenEdgeMovementSpeed = 3f; //spee with screen edge movement
        public float FollowingSpeed = 5f; //speed when following a target
        public float RotationSpeed = 3f;
        public float PanningSpeed = 10f;
        public float MouseRotationSpeed = 10f;

        #endregion

        #region Height

        public bool AutoHeight = true;
        public LayerMask GroundMask = -1; //layermask of ground or other objects that affect height

        public float MinHeight = 10f; //maximal height
        public float MaxHeight = 15f; //minimnal height
        public float HeightDampening = 5f;
        public float KeyboardZoomingSensitivity = 2f;
        public float ScrollWheelZoomingSensitivity = 25f;

        public bool TiltCamera = true;
        public float MinTiltAngle = 30f;
        public float MaxTiltAngle = 60f;

        private float zoomPos; //value in range (0, 1) used as t in Matf.Lerp

        #endregion

        #region MapLimits

        public bool LimitMap = true;
        public float LimitX = 50f; //x limit of map
        public float LimitY = 50f; //z limit of map

        public bool LimitMapOriginalPos;

        #endregion

        #region Targeting

        public Transform TargetFollow; //target to follow
        public Vector3 TargetOffset;

        /// <summary>
        /// are we following target
        /// </summary>
        private bool FollowingTarget { get { return TargetFollow != null; } }

        #endregion

        #region Input

        public bool UseScreenEdgeInput = true;
        public float ScreenEdgeBorder = 25f;

        public bool UseKeyboardInput = true;
        public string HorizontalAxis = "Horizontal";
        public string VerticalAxis = "Vertical";

        public bool UsePanning = true;
        public KeyCode PanningKey = KeyCode.Mouse2;

        public bool UseKeyboardZooming = true;
        public KeyCode ZoomInKey = KeyCode.E;
        public KeyCode ZoomOutKey = KeyCode.Q;

        public bool UseScrollwheelZooming = true;
        public string ZoomingAxis = "Mouse ScrollWheel";

        public bool UseKeyboardRotation = true;
        public KeyCode RotateRightKey = KeyCode.X;
        public KeyCode RotateLeftKey = KeyCode.Z;

        public bool UseMouseRotation = true;
        public KeyCode MouseRotationKey = KeyCode.Mouse1;

        private Vector2 KeyboardInput {
            get {
                return UseKeyboardInput ? new Vector2(Input.GetAxis(HorizontalAxis), Input.GetAxis(VerticalAxis))
                    : Vector2.zero;
            }
        }

        private Vector2 MouseInput { get { return Input.mousePosition; } }

        private float ScrollWheel { get { return Input.GetAxis(ZoomingAxis); } }

        private Vector2 MouseAxis { get { return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); } }

        private int ZoomDirection {
            get {
                bool zoomIn = Input.GetKey(ZoomInKey);
                bool zoomOut = Input.GetKey(ZoomOutKey);
                if (zoomIn && zoomOut) {
                    return 0;
                }
                if (!zoomIn && zoomOut) {
                    return 1;
                }
                return zoomIn ? -1 : 0;
            }
        }

        private int RotationDirection {
            get {
                bool rotateRight = Input.GetKey(RotateRightKey);
                bool rotateLeft = Input.GetKey(RotateLeftKey);
                if (rotateLeft && rotateRight) {
                    return 0;
                }
                if (rotateLeft) {
                    return -1;
                }
                return rotateRight ? 1 : 0;
            }
        }

        #endregion

        #region Unity_Methods

        private void Start() {
            mTransform = transform;
        }

        private void Update() {
            if (!UseFixedUpdate) {
                CameraUpdate();
            }
        }

        private void FixedUpdate() {
            if (UseFixedUpdate) {
                CameraUpdate();
            }
        }

        #endregion

        #region CameraManager_Methods

        /// <summary>
        /// update camera movement and rotation
        /// </summary>
        private void CameraUpdate() {
            if (FollowingTarget) {
                FollowTarget();
            } else {
                Move();
            }

            HeightCalculation();
            Tilt();
            Rotation();
            LimitPosition();
        }

        /// <summary>
        /// move camera with keyboard or with screen edge
        /// </summary>
        private void Move() {
            if (UseKeyboardInput) {
                Vector3 desiredMove = new Vector3(KeyboardInput.x, 0, KeyboardInput.y);

                desiredMove *= KeyboardMovementSpeed;
                desiredMove *= Time.deltaTime;
                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f))*desiredMove;
                desiredMove = mTransform.InverseTransformDirection(desiredMove);

                mTransform.Translate(desiredMove, Space.Self);
            }

            if (UseScreenEdgeInput) {
                Vector3 desiredMove = new Vector3();

                Rect leftRect = new Rect(0, 0, ScreenEdgeBorder, Screen.height);
                Rect rightRect = new Rect(Screen.width - ScreenEdgeBorder, 0, ScreenEdgeBorder, Screen.height);
                Rect upRect = new Rect(0, Screen.height - ScreenEdgeBorder, Screen.width, ScreenEdgeBorder);
                Rect downRect = new Rect(0, 0, Screen.width, ScreenEdgeBorder);

                desiredMove.x = leftRect.Contains(MouseInput) ? -1 : rightRect.Contains(MouseInput) ? 1 : 0;
                desiredMove.z = upRect.Contains(MouseInput) ? 1 : downRect.Contains(MouseInput) ? -1 : 0;

                desiredMove *= ScreenEdgeMovementSpeed;
                desiredMove *= Time.deltaTime;
                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f))*desiredMove;
                desiredMove = mTransform.InverseTransformDirection(desiredMove);

                mTransform.Translate(desiredMove, Space.Self);
            }

            if (UsePanning && Input.GetKey(PanningKey) && MouseAxis != Vector2.zero) {
                Vector3 desiredMove = new Vector3(-MouseAxis.x, 0, -MouseAxis.y);

                desiredMove *= PanningSpeed;
                desiredMove *= Time.deltaTime;
                desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f))*desiredMove;
                desiredMove = mTransform.InverseTransformDirection(desiredMove);

                mTransform.Translate(desiredMove, Space.Self);
            }
        }

        /// <summary>
        /// calcualte height
        /// </summary>
        private void HeightCalculation() {
            float distanceToGround = DistanceToGround();
            if (UseScrollwheelZooming) {
                zoomPos += ScrollWheel*Time.deltaTime*ScrollWheelZoomingSensitivity;
            }
            if (UseKeyboardZooming) {
                zoomPos += ZoomDirection*Time.deltaTime*KeyboardZoomingSensitivity;
            }

            zoomPos = Mathf.Clamp01(zoomPos);

            float targetHeight = Mathf.Lerp(MaxHeight, MinHeight, zoomPos);
            float difference = 0;

            if (Math.Abs(distanceToGround - targetHeight) > 0.001f) {
                difference = targetHeight - distanceToGround;
            }

            mTransform.position = Vector3.Lerp(
                mTransform.position,
                new Vector3(mTransform.position.x, targetHeight + difference, mTransform.position.z),
                Time.deltaTime*HeightDampening);
        }

        /// <summary>
        /// Tilts the camera up or down according to the height above ground for a more bird's eye view.
        /// </summary>
        private void Tilt() {
            var heightPos = (transform.position.y/2f - MinHeight)/(MaxHeight - MinHeight);
            var tiltAngle = Mathf.Lerp(MinTiltAngle, MaxTiltAngle, heightPos);

            var eulerAngles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(tiltAngle, eulerAngles.y, eulerAngles.z);
        }

        /// <summary>
        /// rotate camera
        /// </summary>
        private void Rotation() {
            if (UseKeyboardRotation) {
                transform.Rotate(Vector3.up, RotationDirection*Time.deltaTime*RotationSpeed, Space.World);
            }

            if (UseMouseRotation && Input.GetKey(MouseRotationKey)) {
                mTransform.Rotate(Vector3.up, -MouseAxis.x*Time.deltaTime*MouseRotationSpeed, Space.World);
            }
        }

        /// <summary>
        /// follow targetif target != null
        /// </summary>
        private void FollowTarget() {
            Vector3 targetPos = new Vector3(TargetFollow.position.x, mTransform.position.y, TargetFollow.position.z)
                                + TargetOffset;
            mTransform.position = Vector3.MoveTowards(mTransform.position, targetPos, Time.deltaTime*FollowingSpeed);
        }

        /// <summary>
        /// limit camera position
        /// </summary>
        private void LimitPosition() {
            if (!LimitMap) {
                return;
            }

            mTransform.position = new Vector3(
                Mathf.Clamp(mTransform.position.x, originalPosition.x - LimitX, originalPosition.x + LimitX),
                mTransform.position.y,
                Mathf.Clamp(mTransform.position.z, originalPosition.z - LimitY, originalPosition.z + LimitY));
        }

        /// <summary>
        /// calculate distance to ground
        /// </summary>
        /// <returns></returns>
        private float DistanceToGround() {
            Ray ray = new Ray(mTransform.position, Vector3.down);
            RaycastHit hit;

            return Physics.Raycast(ray, out hit, GroundMask.value) ? (hit.point - mTransform.position).magnitude : 0f;
        }

        #endregion

    }

}
