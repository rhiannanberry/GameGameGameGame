using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Styler))]
public class StylerInspector : Editor
{
    public int selected = 0;
    public override void OnInspectorGUI() {
        var styler = target as Styler;

        selected = styler.selected;


        using (var check = new EditorGUI.ChangeCheckScope()) {
            selected = EditorGUILayout.Popup("Default UI Type", selected, DefaultStyles.defaultStrings);
            
            if (check.changed) {
                serializedObject.FindProperty("selected").intValue = selected;
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("customUIType"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}
