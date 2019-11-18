using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PersistentDataManager : MonoBehaviour
{
    [Scene] public string runOverScene;

    public PlayerSettings debug_playerSettings;
    public MinigameList debug_minigameMasterList;
    public Run debug_run;



    public static PersistentDataManager INSTANCE;
    public static PlayerSettings playerSettings;
    public static MinigameList minigameMasterList;
    private static Run run;

    private bool inRun = false;

    public static Run RUN { get {
        if (RunExists()){
            INSTANCE.inRun = true;
            return run;
        } else {
            INSTANCE.inRun = false;
            return null;
        }
    }}

    public static bool InMinigame { get { return INSTANCE.IsInMinigame() !=null; }}
    
    // MonoBehaviour Methods

    private void Awake() {
        if (minigameMasterList == null) LoadMinigameMasterList();
        if (playerSettings == null) LoadPlayerSettings();
        

        debug_playerSettings = playerSettings;
        debug_minigameMasterList = minigameMasterList;
    }

    private void Start() {
        if (run == null) {

            //Adds ONLY current scene game to list -- for testing
            Minigame m = IsInMinigame();
            if (m != null) {
                //creates run with only this game
                CreateNewRun(new MinigameList(m));
            }
        }
        INSTANCE = this;
        MusicManager._UpdateVolume();
        SoundManager._UpdateVolume();
        
    }

    #region Static Methods

    public static void ClearRun() {
        run = null;
    }
    private static bool RunExists() {
        if (run == null) {
            Debug.LogError("Run does not exist, cannot access Current Game");
            return false;
        }
        return true;
    }

    #endregion

    //Public Methods

    #if UNITY_EDITOR
    public void OnDrawGizmos() {
        if (EditorApplication.isPlayingOrWillChangePlaymode == false) return;

        string s = IsInMinigame() == null ? "IN MENU" : "IN MINIGAME";
        UnityEditor.Handles.BeginGUI();
        GUI.color = Color.white;
        GUIContent st = new GUIContent(s);

        Vector2 stSize = GUI.skin.label.CalcSize(st);

        GUI.Label(new Rect(5,20,stSize.x, stSize.y), s);
        //Vector2 size = GUI.skin.label.CalcSize(new GUIContent("ssttst hshd"));
        //GUI.Label(new Rect(0,0,size.x,size.y), "kdkdkd");
        UnityEditor.Handles.EndGUI();
    }
    #endif


    public void SavePlayerSettings() {
        if (playerSettings != null && SaveSettings.settingsSavingEnabled) {
            FileSaveUtil.SaveData<PlayerSettings>("playerSettings", playerSettings);
        }
    }

    public void SaveMinigameMasterList() {
        if (minigameMasterList != null && SaveSettings.minigameSavingEnabled) {
            FileSaveUtil.SaveData<MinigameList>("minigameMasterList", minigameMasterList);
        }
    }

    public void CreateNewRun(MinigameList runList) {
        run = new Run(runList);
        debug_run = run;
    }

    public void LoadPlayerSettings() {
        playerSettings = FileSaveUtil.LoadData<PlayerSettings>("playerSettings");
        if (playerSettings == null || SaveSettings.settingsSavingEnabled == false) {
            playerSettings = new PlayerSettings();
        }
    }

    // Private Methods
    private void LoadMinigameMasterList() {
        minigameMasterList = FileSaveUtil.LoadData<MinigameList>("minigameMasterList");
        if (minigameMasterList == null || SaveSettings.minigameSavingEnabled == false) {
            minigameMasterList = new MinigameList(true);
        }
    }

    private Minigame IsInMinigame() {
        return minigameMasterList.minigames.Find(item => item.SceneName == SceneManager.GetActiveScene().name);
    }


}
