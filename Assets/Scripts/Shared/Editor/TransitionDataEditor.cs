using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TransitionData))]
public class TransitionDataEditor : Editor
{
    public override void OnInspectorGUI() {
         serializedObject.Update();

        SerializedProperty ease = serializedObject.FindProperty("easingType");
        SerializedProperty curve = serializedObject.FindProperty("customEasingCurve");
        SerializedProperty time = serializedObject.FindProperty("transitionLength");

        EditorGUILayout.PropertyField(ease);
        
        if ( (Ease)ease.intValue == Ease.CUSTOM ) {
            EditorGUILayout.PropertyField(curve);
        }

        EditorGUILayout.PropertyField(time);

        serializedObject.ApplyModifiedProperties();
        
    }
}
