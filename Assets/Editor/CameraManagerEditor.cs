using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DdSG {

    /// <summary>
    /// All credits for this class goes to Denis Sylkin for his RTS Camera:
    /// https://assetstore.unity.com/packages/tools/camera/rts-camera-43321
    /// </summary>
    [CustomEditor(typeof(CameraManager))]
    public class CameraManagerEditor: Editor {

        private CameraManager camera { get { return target as CameraManager; } }
        private TabsBlock tabs;

        private void OnEnable() {
            tabs = new TabsBlock(
                new Dictionary<string, System.Action> {
                    { "Movement", MovementTab },
                    { "Rotation", RotationTab },
                    { "Height", HeightTab }
                });
            tabs.SetCurrentMethod(camera.LastTab);
        }

        public override void OnInspectorGUI() {
            Undo.RegisterCompleteObjectUndo(camera, "CameraManager");
            tabs.Draw();
            if (GUI.changed) {
                camera.LastTab = tabs.CurMethodIndex;
            }
            EditorUtility.SetDirty(camera);
        }

        private void MovementTab() {
            using (new HorizontalBlock()) {
                GUILayout.Label("Use keyboard input: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.UseKeyboardInput = EditorGUILayout.Toggle(camera.UseKeyboardInput);
            }
            if (camera.UseKeyboardInput) {
                camera.HorizontalAxis = EditorGUILayout.TextField("Horizontal axis name: ", camera.HorizontalAxis);
                camera.VerticalAxis = EditorGUILayout.TextField("Vertical axis name: ", camera.VerticalAxis);
                camera.KeyboardMovementSpeed = EditorGUILayout.FloatField(
                    "Movement speed: ",
                    camera.KeyboardMovementSpeed);
            }

            using (new HorizontalBlock()) {
                GUILayout.Label("Screen edge input: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.UseScreenEdgeInput = EditorGUILayout.Toggle(camera.UseScreenEdgeInput);
            }

            if (camera.UseScreenEdgeInput) {
                EditorGUILayout.FloatField("Screen edge border size: ", camera.ScreenEdgeBorder);
                camera.ScreenEdgeMovementSpeed = EditorGUILayout.FloatField(
                    "Screen edge movement speed: ",
                    camera.ScreenEdgeMovementSpeed);
            }

            using (new HorizontalBlock()) {
                GUILayout.Label("Panning with mouse: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.UsePanning = EditorGUILayout.Toggle(camera.UsePanning);
            }
            if (camera.UsePanning) {
                camera.PanningKey = (KeyCode) EditorGUILayout.EnumPopup("Panning when holding: ", camera.PanningKey);
                camera.PanningSpeed = EditorGUILayout.FloatField("Panning speed: ", camera.PanningSpeed);
            }

            using (new HorizontalBlock()) {
                GUILayout.Label("Limit movement: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.LimitMap = EditorGUILayout.Toggle(camera.LimitMap);
            }
            if (camera.LimitMap) {
                camera.LimitMapOriginalPos = EditorGUILayout.Toggle(
                    "Base on camera's original position: ",
                    camera.LimitMapOriginalPos);
                camera.LimitX = EditorGUILayout.FloatField("Limit X: ", camera.LimitX);
                camera.LimitY = EditorGUILayout.FloatField("Limit Y: ", camera.LimitY);
            }

            GUILayout.Label("Follow target", EditorStyles.boldLabel);
#pragma warning disable 618
            camera.TargetFollow = EditorGUILayout.ObjectField(
#pragma warning restore 618
                "Target to follow: ",
                camera.TargetFollow,
                typeof(Transform)) as Transform;
            camera.TargetOffset = EditorGUILayout.Vector3Field("Target offset: ", camera.TargetOffset);
            camera.FollowingSpeed = EditorGUILayout.FloatField("Following speed: ", camera.FollowingSpeed);
        }

        private void RotationTab() {
            using (new HorizontalBlock()) {
                GUILayout.Label("Keyboard input: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.UseKeyboardRotation = EditorGUILayout.Toggle(camera.UseKeyboardRotation);
            }
            if (camera.UseKeyboardRotation) {
                camera.RotateLeftKey = (KeyCode) EditorGUILayout.EnumPopup("Rotate left: ", camera.RotateLeftKey);
                camera.RotateRightKey = (KeyCode) EditorGUILayout.EnumPopup("Rotate right: ", camera.RotateRightKey);
                camera.RotationSpeed = EditorGUILayout.FloatField("Keyboard rotation speed", camera.RotationSpeed);
            }

            using (new HorizontalBlock()) {
                GUILayout.Label("Mouse input: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.UseMouseRotation = EditorGUILayout.Toggle(camera.UseMouseRotation);
            }
            if (camera.UseMouseRotation) {
                camera.MouseRotationKey = (KeyCode) EditorGUILayout.EnumPopup(
                    "Mouse rotation key: ",
                    camera.MouseRotationKey);
                camera.MouseRotationSpeed = EditorGUILayout.FloatField(
                    "Mouse rotation speed: ",
                    camera.MouseRotationSpeed);
            }
        }

        private void HeightTab() {
            using (new HorizontalBlock()) {
                GUILayout.Label("Auto height: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.AutoHeight = EditorGUILayout.Toggle(camera.AutoHeight);
            }
            if (camera.AutoHeight) {
                camera.HeightDampening = EditorGUILayout.FloatField("Height dampening: ", camera.HeightDampening);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("GroundMask"));
            }

            using (new HorizontalBlock()) {
                GUILayout.Label("Keyboard zooming: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.UseKeyboardZooming = EditorGUILayout.Toggle(camera.UseKeyboardZooming);
            }
            if (camera.UseKeyboardZooming) {
                camera.ZoomInKey = (KeyCode) EditorGUILayout.EnumPopup("Zoom In: ", camera.ZoomInKey);
                camera.ZoomOutKey = (KeyCode) EditorGUILayout.EnumPopup("Zoom Out: ", camera.ZoomOutKey);
                camera.KeyboardZoomingSensitivity = EditorGUILayout.FloatField(
                    "Keyboard sensitivity: ",
                    camera.KeyboardZoomingSensitivity);
            }

            using (new HorizontalBlock()) {
                GUILayout.Label("Scrollwheel zooming: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.UseScrollwheelZooming = EditorGUILayout.Toggle(camera.UseScrollwheelZooming);
            }
            if (camera.UseScrollwheelZooming) {
                camera.ScrollWheelZoomingSensitivity = EditorGUILayout.FloatField(
                    "Scrollwheel sensitivity: ",
                    camera.ScrollWheelZoomingSensitivity);
            }

            if (camera.UseScrollwheelZooming || camera.UseKeyboardZooming) {
                using (new HorizontalBlock()) {
                    camera.MinHeight = EditorGUILayout.FloatField("Min height: ", camera.MinHeight);
                    camera.MaxHeight = EditorGUILayout.FloatField("Max height: ", camera.MaxHeight);
                }
            }

            using (new HorizontalBlock()) {
                GUILayout.Label("Camera tilting on zoom: ", EditorStyles.boldLabel, GUILayout.Width(170f));
                camera.TiltCamera = EditorGUILayout.Toggle(camera.TiltCamera);
            }
            if (camera.TiltCamera) {
                using (new HorizontalBlock()) {
                    camera.MinTiltAngle = EditorGUILayout.FloatField("Min tilt angle: ", camera.MinTiltAngle);
                    camera.MaxTiltAngle = EditorGUILayout.FloatField("Max tilt angle: ", camera.MaxTiltAngle);
                }
            }
        }

    }

}
