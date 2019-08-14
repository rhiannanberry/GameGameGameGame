using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Run : System.Object
{
    [SerializeField] private MinigameList _runList;
    [SerializeField] private int _runLength;
    [SerializeField] private int _minigameIndex;
    [SerializeField] private int _lives;

    public Run(MinigameList runList) {
        _runList = runList;
        _runLength = runList.Count;
        _minigameIndex = 0;
        _lives = 3;
    }


    //Properties
    public Minigame CurrentGame { get { return _runList.minigames[_minigameIndex]; }}
    public int Lives { get {return _lives; }}


    //Public Methods
    public void GameWon() {
        float timeTaken = CurrentGame.TimeLimit - Timer.time;
        bool isNewRecord = _runList.minigames[_minigameIndex].UpdateWinTime(timeTaken);
        RunResult();
    }

    public void GameLost() {
        _runList.minigames[_minigameIndex].SetGamePlayed();
        _lives--;
        RunResult();
    }

    public string CurrentScene() {
        return CurrentGame.SceneName;
    }

    public string NextScene() {
        if (HasNextGame()) {
            _minigameIndex += 1;
            return _runList.minigames[_minigameIndex].SceneName;
        }
        return "RunOverScene";
    }

    public void ResetRun() {
        _lives = 3;
        _minigameIndex = 0;
    }

    //Private Methods

    private void RunResult() {
        if ( _lives == 0 || !HasNextGame() ) {
            Debug.Log("RUN END");
        } else {
            Debug.Log("RUN CONTINUE");
        }
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }

    private bool HasNextGame() {
        return _minigameIndex + 1 != _runLength;
    }
}
