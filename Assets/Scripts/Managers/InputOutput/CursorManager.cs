using UnityEngine;

namespace DdSG {

    public class CursorManager: SingletonBehaviour<CursorManager> {

        protected CursorManager() {
        }

        [Header("Attributes")]
        public CursorMode CursorMode = CursorMode.Auto;

        [Header("Unity Setup Fields: Cursors")]
        public Texture2D Default;
        public Vector2 DefaultHotSpot = new Vector2(4f, 2f);
        public Texture2D Pointer;
        public Vector2 PointerHotSpot = new Vector2(11f, 2f);
        public Texture2D Target;
        public Vector2 TargetHotSpot = new Vector2(16f, 16f);

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members
        private CursorType currentCursorType;

        public void SetCursor(CursorType cursorType) {
            currentCursorType = cursorType;
            SetTemporaryCursor(cursorType);
        }

        public void ResetCursor() {
            SetCursor(CursorType.Default);
        }

        public void SetTemporaryCursor(CursorType cursorType) {
            switch (cursorType) {
            case CursorType.Pointer:
                Cursor.SetCursor(Pointer, PointerHotSpot, CursorMode);
                break;
            case CursorType.Target:
                Cursor.SetCursor(Target, TargetHotSpot, CursorMode);
                break;
            default:
                Cursor.SetCursor(Default, DefaultHotSpot, CursorMode);
                break;
            }
        }

        public void ResetTemporaryCursor() {
            SetCursor(currentCursorType);
        }

    }

}
