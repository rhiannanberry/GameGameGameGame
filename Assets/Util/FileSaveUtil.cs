using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
public static class FileSaveUtil
{
    public static T LoadData<T>(string saveName) {
        #if !UNITY_STANDALONE && !UNITY_EDITOR
        return default(T);
        #endif

        if (File.Exists(FullPath(saveName))) {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(FullPath(saveName), FileMode.Open);
                T data = (T)bf.Deserialize(file);
                file.Close();
                Debug.Log("DATA LOADED");
                return data;

            } 
        return default(T);
    }

    public static void SaveData<T>(string saveName, T data) {
        #if !UNITY_STANDALONE && !UNITY_EDITOR
        return;
        #endif

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(FullPath(saveName));
        bf.Serialize(file, data);
        Debug.Log("DATA SAVED");
        file.Close();
        
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
        #endif

    }

    public static bool Exists(string saveName) {
        return File.Exists(FullPath(saveName));
    }

    public static void Delete(string saveName) {
        if (Exists(saveName)) {
            File.Delete(FullPath(saveName));
            UnityEditor.AssetDatabase.Refresh();
        }
    }

    private static string FullPath(string saveName) {
        return Application.dataPath + "/" + saveName + ".save";
    }
   
}
