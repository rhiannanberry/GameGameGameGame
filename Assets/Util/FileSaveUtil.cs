using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
public static class FileSaveUtil
{
    public static T LoadData<T>(string saveName) {
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
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(FullPath(saveName));
        bf.Serialize(file, data);
        Debug.Log("DATA SAVED");
        file.Close();
    }

    private static string FullPath(string saveName) {
        return Application.dataPath + "/" + saveName + ".save";
    }
   
}
