using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Run : System.Object
{
    #region Properties
    [SerializeField] private MinigameList _runList;
    [SerializeField] private int _runLength;
    [SerializeField] private int _minigameIndex;
    [SerializeField] private int _lives;
    
    private bool _runOver = false;
    private bool _gameWon = false;
    private bool _exitEarly = false;

    public Minigame CurrentGame { get { return _runList.minigames[_minigameIndex]; }}
    public string CurrentScene { get { return CurrentGame.SceneName; }}
    public int Lives { get {return _lives; }}
    public bool WonGame { get { return _gameWon; }}
    #endregion

    public Run(MinigameList runList) {
        _runList = runList;
        _runLength = runList.Count;
        _minigameIndex = 0;
        _lives = 3;
        Timers.ResetTimers();
    }


    

    //Public Methods

    public void ExitEarly() {
        _exitEarly = true;
        GameStateManager.INSTANCE.TriggerStateChange(GameState.INGAME);
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }

    public void GameWon() {
        float timeTaken = CurrentGame.TimeLimit - HUDDetails.time;
        bool isNewRecord = _runList.minigames[_minigameIndex].UpdateWinTime(timeTaken);
        _gameWon = true;
        RunResult();
    }

    public void TimeRanOut() {
        Minigame m = _runList.minigames[_minigameIndex];
        if (m.Survival == false && m.ScoreBased == false) {
            GameLost();
        } else if (m.ScoreBased && m.ScoreMet()) {
            GameWon();
        } else if (m.Survival) {
            GameWon();
        } else {
            GameLost();
        }
    }

    public void GameLost() {
        _runList.minigames[_minigameIndex].SetGamePlayed();
        _lives--;
        _gameWon = false;
        RunResult();
    }


    public string NextScene() {
        if (_exitEarly || _runOver) {
            return PersistentDataManager.INSTANCE.runOverScene;
        }
        if (HasNextGame()) {
            _minigameIndex += 1;
            return _runList.minigames[_minigameIndex].SceneName;
        }
        return PersistentDataManager.INSTANCE.runOverScene;
    }


    public void ResetRun() {
        _lives = 3;
        _minigameIndex = 0;
        Timers.ResetTimers();
    }

    //Private Methods

    private void RunResult() {
        CurrentGame.ResetScore();
        if ( _lives == 0 || !HasNextGame() ) {
            Debug.Log("RUN END");
            _runOver = true;
        } else {
            Debug.Log("RUN CONTINUE");
        }
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }

    private bool HasNextGame() {
        return _minigameIndex + 1 != _runLength;
    }
}
