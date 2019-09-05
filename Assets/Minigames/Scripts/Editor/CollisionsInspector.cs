using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Collisions))]
public class CollisionsInspector : Editor
{
    int resultInt;
    SerializedProperty unityevents;

    void OnEnable() {
        unityevents = serializedObject.FindProperty("customCollisionResults");
    }
    public override void OnInspectorGUI() {
        Collisions c = (Collisions)target;

        EditorGUILayout.LabelField("Collision Result Type");

        Result[] results = (Result[])Enum.GetValues(typeof(Result));
        bool[] resultsBool = new bool[results.Length - 1];
        for(int i = 0; i < results.Length - 1; i++) {
            resultsBool[i] = results[i] == c.collisionResult;
        }
        EditorGUI.indentLevel++;
        for(int i = 0; i < results.Length - 1; i++) {
            bool isCurrentResult = results[i] == c.collisionResult;
            resultsBool[i] = EditorGUILayout.Toggle(results[i].ToString(), resultsBool[i]);
            if (!isCurrentResult && resultsBool[i]) {
                if (c.collisionResult != Result.Custom)
                    resultsBool[(int)c.collisionResult] = false;
                c.collisionResult = results[i]; 
            } else if (isCurrentResult && !resultsBool[i]) {
                c.collisionResult = Result.Custom;
            }
        }
        
        c.useCustom = c.useCustom || c.collisionResult == Result.Custom;
        
        c.useCustom = EditorGUILayout.Toggle("Custom", c.useCustom);
        
        EditorGUI.indentLevel--;

        if (c.useCustom) {
            EditorGUILayout.PropertyField(unityevents, new GUIContent("Custom Collision Results"));
            serializedObject.ApplyModifiedProperties();
        }


        DrawDefaultInspector();
        
    }
}
