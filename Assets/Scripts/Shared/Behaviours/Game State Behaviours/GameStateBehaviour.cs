using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateBehaviour : MonoBehaviour
{
    protected virtual GameState _event { get; }
    protected virtual void Start() {
        GameStateManager.INSTANCE.StartListeningStateEnter(_event, OnStateEnter);
        GameStateManager.INSTANCE.StartListeningStateExit(_event, OnStateExit);
    }

    protected virtual void OnDisable() {
        GameStateManager.INSTANCE.StopListeningStateEnter(_event, OnStateEnter);
        GameStateManager.INSTANCE.StopListeningStateExit(_event, OnStateExit);
    }

    protected abstract void OnStateEnter();

    protected abstract void OnStateExit();

}
