using UnityEditor;
using UnityEngine;
using System;

[CustomEditor(typeof(MinigameScriptableObject), true)]
public class MinigameScriptableObjectInspector : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        var minigame = target as MinigameScriptableObject;
        var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(minigame._scenePath);

        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        var newScene = EditorGUILayout.ObjectField("Scene", oldScene, typeof(SceneAsset), false) as SceneAsset;

        if (EditorGUI.EndChangeCheck())
        {
            string newPath = AssetDatabase.GetAssetPath(newScene);
            var scenePathProperty = serializedObject.FindProperty("_scenePath");
            scenePathProperty.stringValue = newPath;
            var sceneNameProperty = serializedObject.FindProperty("_sceneName");
            
            Debug.Log(newPath.ToString());
            string[] sceneName = newPath.Split('/');
                 
            sceneNameProperty.stringValue = sceneName[sceneName.Length - 1].Replace(".unity", "");
        }
        serializedObject.ApplyModifiedProperties();

    }
}
