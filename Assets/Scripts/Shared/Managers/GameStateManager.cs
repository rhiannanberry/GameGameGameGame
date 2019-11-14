using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameState;

public class GameStateManager : MonoBehaviour
{
    private GameState _state;
    private ListenerDictionary<GameState> _enterStateListeners; 
    private ListenerDictionary<GameState> _exitStateListeners; 

    private Animator _animator;


    public static GameStateManager INSTANCE;

    public void SetState(GameState state) {
        _state = state;
    }

    private void Awake() {
        if (INSTANCE == null) {
            INSTANCE = this;
        }
        _enterStateListeners = new ListenerDictionary<GameState>();
        _exitStateListeners = new ListenerDictionary<GameState>();

    }

    private void Start() {
        _animator = GetComponent<Animator>();
    }

    public void StartListeningStateEnter(GameState s, Action a) {
        _enterStateListeners.AddListener(s, a);
    }

    public void StartListeningStateExit(GameState s, Action a) {
        _exitStateListeners.AddListener(s, a);
    }

    public void StopListeningStateEnter(GameState s, Action a) {
        _enterStateListeners.RemoveListener(s, a);
    }

    public void StopListeningStateExit(GameState s, Action a) {
        _exitStateListeners.RemoveListener(s, a);
    }

    public void OnStateEnter(GameState enteringState) {
        _state = enteringState;
        _enterStateListeners.NotifyListeners(enteringState);
        Debug.Log("Entering: " + enteringState.ToString());
    }

    public void OnStateExit(GameState exitingState) {
        _exitStateListeners.NotifyListeners(exitingState);
        //Debug.Log("Exiting: " + exitingState.ToString());
    }


    public void TriggerStateChange(GameState s) {
        TriggerStateChange(s.ToString());
    }

    public void TriggerStateChange(string s) {
        _animator.SetTrigger(s);
    }
}
