using UnityEngine;

namespace DdSG {

    public class CursorManager: SingletonBehaviour<CursorManager> {

        [Header("Attributes")]
        public CursorMode CursorMode = CursorMode.Auto;

        [Header("Unity Setup Fields: Cursors")]
        public Texture2D Default;
        public Vector2 DefaultHotSpot = new Vector2(4f, 2f);
        public Texture2D Pointer;
        public Vector2 PointerHotSpot = new Vector2(11f, 2f);

        //[Header("Optional")]

        // Public members hidden from Unity Inspector
        //[HideInInspector]

        // Private and protected members

        public void SetCursor(CursorType cursorType) {
            switch (cursorType) {
            case CursorType.Pointer:
                Cursor.SetCursor(Pointer, PointerHotSpot, CursorMode);
                break;
            default:
                Cursor.SetCursor(Default, DefaultHotSpot, CursorMode);
                break;
            }
        }

    }

}
