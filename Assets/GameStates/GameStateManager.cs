using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameState;

public class GameStateManager : MonoBehaviour
{
    private GameState _state;

    private ListenerList<GameState> _enterStateListeners;
    //private ListenerDictionary<GameState> _enterStateListeners; 
    private ListenerList<GameState> _exitStateListeners; 



    private Animator _animator;


    public static GameStateManager INSTANCE;

    public void SetState(GameState state) {
        _state = state;
    }

    private void Awake() {
        if (INSTANCE == null) {
            INSTANCE = this;
        }
        _enterStateListeners = new ListenerList<GameState>();
        _exitStateListeners = new ListenerList<GameState>();

    }

    private void Start() {
        _animator = GetComponent<Animator>();
    }

    public void StartListeningStateEnter(Action<GameState> a) {
        _enterStateListeners.AddListener(a);
    }

    public void StartListeningStateExit(Action<GameState> a) {
        _exitStateListeners.AddListener(a);
    }

    public void StopListeningStateEnter(Action<GameState> a) {
        _enterStateListeners.RemoveListener(a);
    }

    public void StopListeningStateExit(Action<GameState> a) {
        _exitStateListeners.RemoveListener(a);
    }

    public void OnStateEnter(GameState enteringState) {
        _state = enteringState;
        _enterStateListeners.NotifyListeners(enteringState);
        Debug.Log("Entering: " + enteringState.ToString());
    }

    public void OnStateExit(GameState exitingState) {
        _exitStateListeners.NotifyListeners(exitingState);
        Debug.Log("Exiting: " + exitingState.ToString());
    }


    public void TriggerStateChange(GameState s) {
        TriggerStateChange(s.ToString());
    }

    public void TriggerStateChange(string s) {
        _animator.SetTrigger(s);
    }
}
