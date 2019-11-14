using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunOverInScene : InSceneBehaviour
{
    protected override void Start() {
        base.Start();
        foreach(Button b in GetComponentsInChildren<Button>()) {
            b.interactable = false;
        }
    }

    protected override void OnStateEnter() {
        foreach(Button b in GetComponentsInChildren<Button>()) {
            b.interactable = true;
        }
    }

    protected override void OnStateExit() {
        foreach(Button b in GetComponentsInChildren<Button>()) {
            b.interactable = false;
        }
    }


    public void RestartRun() {
        RunOverExiting.restarting = true;
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }

    public void BackToMainMenu() {
        RunOverExiting.restarting = false;
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }
}
