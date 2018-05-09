using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

/// <summary>
/// All credits for this class goes to Denis Sylkin for his RTS Camera:
/// https://assetstore.unity.com/packages/tools/camera/rts-camera-43321
/// </summary>
public class VerticalBlock: IDisposable {

    public VerticalBlock(GUIStyle style, params GUILayoutOption[] options) {
        GUILayout.BeginVertical(style, options);
    }

    public void Dispose() {
        GUILayout.EndVertical();
    }

}

/// <summary>
/// All credits for this class goes to Denis Sylkin for his RTS Camera:
/// https://assetstore.unity.com/packages/tools/camera/rts-camera-43321
/// </summary>
public class HorizontalBlock: IDisposable {

    public HorizontalBlock(params GUILayoutOption[] options) {
        GUILayout.BeginHorizontal(options);
    }

    public void Dispose() {
        GUILayout.EndHorizontal();
    }

}

/// <summary>
/// All credits for this class goes to Denis Sylkin for his RTS Camera:
/// https://assetstore.unity.com/packages/tools/camera/rts-camera-43321
/// </summary>
public class ColoredBlock: IDisposable {

    public ColoredBlock(Color color) {
        GUI.color = color;
    }

    public void Dispose() {
        GUI.color = Color.white;
    }

}

/// <summary>
/// All credits for this class goes to Denis Sylkin for his RTS Camera:
/// https://assetstore.unity.com/packages/tools/camera/rts-camera-43321
/// </summary>
[Serializable]
public class TabsBlock {

    private readonly Dictionary<string, Action> methods;
    private Action currentGuiMethod;
    public int CurMethodIndex = -1;

    public TabsBlock(Dictionary<string, Action> methods) {
        this.methods = methods;
        SetCurrentMethod(0);
    }

    public void Draw() {
        string[] keys = methods.Keys.ToArray();
        using (new VerticalBlock(EditorStyles.helpBox)) {
            using (new HorizontalBlock()) {
                for (int i = 0; i < keys.Length; i++) {
                    var btnStyle = i == 0 ? EditorStyles.miniButtonLeft :
                        i == (keys.Length - 1) ? EditorStyles.miniButtonRight : EditorStyles.miniButtonMid;
                    using (new ColoredBlock(currentGuiMethod == methods[keys[i]] ? Color.grey : Color.white)) {
                        if (GUILayout.Button(keys[i], btnStyle)) {
                            SetCurrentMethod(i);
                        }
                    }
                }
            }
            GUILayout.Label(keys[CurMethodIndex], EditorStyles.centeredGreyMiniLabel);
            currentGuiMethod();
        }
    }

    public void SetCurrentMethod(int index) {
        CurMethodIndex = index;
        currentGuiMethod = methods[methods.Keys.ToArray()[index]];
    }

}
