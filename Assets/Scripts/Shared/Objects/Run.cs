﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Run : System.Object
{
    [SerializeField] private MinigameList _runList;
    [SerializeField] private int _runLength;
    [SerializeField] private int _minigameIndex;
    [SerializeField] private int _lives;
    private bool runOver = false;
    [HideInInspector] public bool gameWon = false;
    [HideInInspector] public bool exitEarly = false;

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
        float timeTaken = CurrentGame.TimeLimit - TimerBehaviour.time;
        bool isNewRecord = _runList.minigames[_minigameIndex].UpdateWinTime(timeTaken);
        gameWon = true;
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
        gameWon = false;
        RunResult();
    }

    public string CurrentScene() {
        return CurrentGame.SceneName;
    }

    public string NextScene() {
        if (exitEarly || runOver) {
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
    }

    //Private Methods

    private void RunResult() {
        CurrentGame.ResetScore();
        if ( _lives == 0 || !HasNextGame() ) {
            Debug.Log("RUN END");
            runOver = true;
        } else {
            Debug.Log("RUN CONTINUE");
        }
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }

    private bool HasNextGame() {
        Debug.Log(_minigameIndex);
        return _minigameIndex + 1 != _runLength;
    }
}
