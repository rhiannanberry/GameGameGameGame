using UnityEditor;
using UnityEngine;
using System.IO;


public class CreateTemplateScene : MonoBehaviour
{
    private static string scenesFolder = "Resources/Scenes/";

    [MenuItem("Assets/Create/Scene From Template", false, 0)]
    public static void Empty() {

    }

    [MenuItem("Assets/Create/Scene From Template/3D Minigame", false, 0)]
    public static void CreateMinigame3DSceneFromTemplate() {
        string path = "Assets/Resources/Scenes/";
    
        AssetDatabase.CopyAsset(path + "TEMPLATE_minigame_3d.unity", GetNewScenePath());
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create/Scene From Template/2D Minigame", false, 1)]
    public static void CreateMinigame2DSceneFromTemplate() {
        string path = "Assets/Resources/Scenes/";
    
        AssetDatabase.CopyAsset(path + "TEMPLATE_minigame_2d.unity", GetNewScenePath());
        AssetDatabase.Refresh();
    }

    private static string GetNewScenePath() {
        string fullpath = Application.dataPath + "/" + scenesFolder;
        string sceneName = "yourname_gameName_scene";
        while (File.Exists(fullpath + sceneName + ".unity")) {
            sceneName += "_copy";
        }
        return "Assets/" + scenesFolder + sceneName + ".unity";
    }
}
