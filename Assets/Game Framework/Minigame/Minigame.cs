using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Minigame : System.Object
{
    // Member Variables
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private string _author;
    [SerializeField] private string _sceneName;
    [SerializeField] private float _timeLimit;
    [SerializeField] private float _lowestWinTime;
    [SerializeField] private bool _gamePlayed;
    [SerializeField] private bool _gameWon;

    // Getters
    public string Name {get {return _name; }}
    public string Description {get {return _description; }}
    public string Author {get {return _author; }}
    public string SceneName {get {return _sceneName; }}
    public float TimeLimit {get {return _timeLimit; }}
    public float LowestWinTime {get {return _lowestWinTime; }}
    public bool GamePlayed {get {return _gamePlayed; }}
    public bool GameWon {get {return _gameWon; }}


    // Constructors

    // Full Constructor
    public Minigame(string gameName, string description, string author, string sceneName, float timeLimit, float lowestWinTime, bool gamePlayed, bool gameWon) {
        _name = gameName;
        _description = description;
        _author = author;
        _sceneName = sceneName;
        _timeLimit = timeLimit;
        _lowestWinTime = lowestWinTime;
        _gamePlayed = gamePlayed;
        _gameWon = gameWon;
    }

    //New-save Constructor
    public Minigame(string gameName, string description, string author, string sceneName, float timeLimit) : 
        this(gameName, description, author, sceneName, timeLimit, -1, false, false) {
        }


    // Public Checking Functions

    public bool IsUnderTimeLimit(float currentTime) {
        return currentTime <= _timeLimit;
    }

    public override bool Equals(object obj) {
        if ( (obj == null) || !this.GetType().Equals(obj.GetType()) ) return false;
        return (_name == ((Minigame)obj).Name);
    }

    public override int GetHashCode() {
        var hashCode = 352033288;
        return hashCode * -1521134295 + _name.GetHashCode();
    }

    //Public Setting Functions
    public bool UpdateWinTime(float newWinTime) {
        if (_gameWon) {
            if (_lowestWinTime > newWinTime) {
                _lowestWinTime = newWinTime;
                return true; //Win Time Updated
            }
        } else {
            _gamePlayed = true;
            _gameWon = true;
            _lowestWinTime = newWinTime;
            return true;
        }
        return false;
    }

    public void SetGamePlayed() {
        _gamePlayed = true;
    }

    public void UpdateDetails(Minigame other) {
        if (this.Equals(other)) {
            _timeLimit = other.TimeLimit;
            _lowestWinTime = other.LowestWinTime;
            _gamePlayed = other.GamePlayed;
            _gameWon = other.GameWon;
        }
    }

    
}
