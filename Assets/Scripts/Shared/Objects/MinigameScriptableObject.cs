using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is ONLY used and updated in the EDITOR and on NEW-SAVE GENERATION

[CreateAssetMenu(fileName = "Minigame", menuName = "Scriptable Objects/MinigameScriptableObject", order = 1)]
public class MinigameScriptableObject : ScriptableObject
{
    public string _name;
    [TextArea] public string _description;
    public string _author;
    [HideInInspector] public string _scenePath;
    [HideInInspector] public string _sceneName;
    public float _timeLimit;

    public Minigame ToObject() {
        return new Minigame(_name, _description, _author, _sceneName, _timeLimit);
    }
}
