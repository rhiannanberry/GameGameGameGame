using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[InitializeOnLoadAttribute]
public class SaveSettingsEditor : EditorWindow
{
    private bool _playing = false;
    private bool settingsSavingEnabled;
    private bool minigameSavingEnabled;

    [MenuItem ("Window/Save Settings")]
    public static void  ShowWindow () {
        EditorWindow.GetWindow(typeof(SaveSettingsEditor), false, "Save Settings", true);
    }
    
    void OnGUI () {
        if (EditorApplication.isPlaying && !_playing) {
            PersistentDataManager.settingsSavingEnabled = settingsSavingEnabled;
            PersistentDataManager.minigameSavingEnabled = minigameSavingEnabled;
        }
        PersistentDataManager.settingsSavingEnabled = EditorGUILayout.Toggle("Enable Settings Saving", PersistentDataManager.settingsSavingEnabled);
        PersistentDataManager.minigameSavingEnabled = EditorGUILayout.Toggle("Enable Minigame Saving", PersistentDataManager.minigameSavingEnabled);

        settingsSavingEnabled = PersistentDataManager.settingsSavingEnabled;
        minigameSavingEnabled = PersistentDataManager.minigameSavingEnabled;

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
