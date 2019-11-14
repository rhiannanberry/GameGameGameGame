using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ColorPaletteSO))]
public class ColorPaletteInspector : Editor
{
    private static GUIContent refreshButtonContent = new GUIContent("Reload Defaults", "reload defaults");

    public override void OnInspectorGUI() {
        if (GUILayout.Button(refreshButtonContent)) {
            var palette = target as ColorPaletteSO;
            palette.UpdateDefaults();
        }
        serializedObject.Update();
        EditorList.Show(serializedObject.FindProperty("defaults"), false);
        EditorList.Show(serializedObject.FindProperty("customs"), true);
        serializedObject.ApplyModifiedProperties();
    }
}
