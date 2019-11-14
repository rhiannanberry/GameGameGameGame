using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[InitializeOnLoadAttribute]
public class SaveSettingsEditor : EditorWindow
{
    [MenuItem ("Window/Save Settings")]
    public static void  ShowWindow () {
        EditorWindow.GetWindow(typeof(SaveSettingsEditor), false, "Save Settings", true);
    }
    
    void OnGUI () {
        if (!EditorPrefs.HasKey("settingsSavingEnabled")) {
            EditorPrefs.SetBool("settingsSavingEnabled", false);
            EditorPrefs.SetBool("minigameSavingEnabled", false);
        }
        EditorPrefs.SetBool("settingsSavingEnabled", EditorGUILayout.Toggle("Enable Settings Saving",EditorPrefs.GetBool("settingsSavingEnabled")));
        EditorPrefs.SetBool("minigameSavingEnabled", EditorGUILayout.Toggle("Enable Minigame Saving",EditorPrefs.GetBool("minigameSavingEnabled")));

        SaveSettings.settingsSavingEnabled = EditorPrefs.GetBool("settingsSavingEnabled");
        SaveSettings.minigameSavingEnabled = EditorPrefs.GetBool("minigameSavingEnabled");

        using (new EditorGUI.DisabledScope(!FileSaveUtil.Exists("playerSettings"))) {
            if (GUILayout.Button("Delete Player Settings")) {
                FileSaveUtil.Delete("playerSettings");
            }
        }

        using (new EditorGUI.DisabledScope(!FileSaveUtil.Exists("minigameMasterList"))) {
            if (GUILayout.Button("Delete Minigame Saves")) {
                FileSaveUtil.Delete("minigameMasterList");
            }
        }
    }
}
