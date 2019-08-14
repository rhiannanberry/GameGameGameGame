using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class MinigameList 
{
    public List<Minigame> minigames;

    public MinigameList() {
        minigames = new List<Minigame>();
    }

    public MinigameList(List<MinigameScriptableObject> soList) : this() {
        foreach(MinigameScriptableObject m in soList) {
            minigames.Add(m.ToObject());
        }
    }

    public MinigameList(params Minigame[] minigamesArray) {
        minigames = minigamesArray.ToList();
    }

    public void UpdateList(MinigameList otherList) {
        foreach(Minigame other in otherList.minigames) {
            minigames.First( item => item.Name == other.Name).UpdateDetails(other);
        }
    }

    public int Count { get {return minigames.Count; }}
    /*private void Start() {

        if (minigames == null) {
            Debug.Log("GENERATING");
            GenerateNewMinigameList();
        }

        #if (UNITY_EDITOR)
            debug_minigames = minigames.ToArray();
        #endif
    }*/

    //TODO: Include check for saved minigame data
}
