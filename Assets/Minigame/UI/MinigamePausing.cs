using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePausing : GameStateBehaviourNew
{
    [SerializeField] private GameObject _pauseCanvas;
    private bool _paused = false;


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            
            if (_paused) {
                ReturnToGame();
            } else {
                GameStateManager.INSTANCE.TriggerStateChange(GameState.INPAUSE);
            }
        }
    }

    public void OnStateEnterENTERING() {
        _pauseCanvas.SetActive(false);
    }

    public void OnStateEnterINPAUSE() {
        _paused = true;
        _pauseCanvas.SetActive(true);
    }

    public void OnStateExitINPAUSE() {
        _paused = false;
        _pauseCanvas.SetActive(false);
    }

    public void ExitRun() {
        GameStateManager.INSTANCE.TriggerStateChange(GameState.INGAME);
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }

    public void ReturnToGame() {
        GameStateManager.INSTANCE.TriggerStateChange(GameState.INGAME);
    }
}
