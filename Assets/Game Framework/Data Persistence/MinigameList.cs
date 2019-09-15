using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;


[System.Serializable]
public class MinigameList 
{
    private static readonly System.Random rnd = new System.Random();
    public List<Minigame> minigames;

    // Creates an empty list
    public MinigameList() {
        minigames = new List<Minigame>();
    }

    public MinigameList(bool populate) : this() {
        if (populate) {
            MinigameScriptableObject[] soArray = Resources.LoadAll<MinigameScriptableObject>("Minigames");
            foreach(MinigameScriptableObject m in soArray) {
                minigames.Add(m.ToObject());
            }
        }
    }

    public MinigameList(List<MinigameScriptableObject> soList) : this() {
        foreach(MinigameScriptableObject m in soList) {
            minigames.Add(m.ToObject());
        }
    }

    public MinigameList(List<Minigame> mList) {
        minigames = mList;
    }

    public MinigameList(params Minigame[] minigamesArray) {
        minigames = minigamesArray.ToList();
    }

    public void UpdateList(MinigameList otherList) {
        foreach(Minigame other in otherList.minigames) {
            minigames.First( item => item.Name == other.Name).UpdateDetails(other);
        }
    }

    public MinigameList RandomReorder(Minigame first) {
        List<Minigame> newList = minigames;
        newList.Shuffle();
        if (newList.Contains(first)) newList.Remove(first);
        newList.Insert(0, first);
        return new MinigameList(newList);
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
