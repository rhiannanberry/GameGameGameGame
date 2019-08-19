using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public abstract class GameStateBehaviourNew : MonoBehaviour
{
    private List<Action<GameState>> _enteringEvents;
    private List<Action<GameState>> _exitingEvents;

    protected virtual void Start()
    {
        _enteringEvents = new List<Action<GameState>>();
        _exitingEvents = new List<Action<GameState>>();
        
        
        MethodInfo enterMethod = this.GetType().GetMethod( "OnStateEnter" );
        MethodInfo exitMethod = this.GetType().GetMethod( "OnStateExit" );

        if ( enterMethod != null ) {
            Action<GameState> enterAction = (Action<GameState>) Delegate.CreateDelegate(typeof(Action), this, enterMethod);
            _enteringEvents.Add(enterAction);
        }
        if ( exitMethod != null ) {
            Action<GameState> exitAction = (Action<GameState>) Delegate.CreateDelegate(typeof(Action), this, exitMethod);
            _exitingEvents.Add(exitAction);
        }
        

        AddListeners();
    }

    void OnDisable() {
        RemoveListeners();
    }

    private void AddListeners() {

        foreach(Action<GameState> a in _enteringEvents) {
            GameStateManager.INSTANCE.StartListeningStateEnter(a);
        }
        foreach(Action<GameState> a in _exitingEvents) {
            GameStateManager.INSTANCE.StartListeningStateExit(a);
        }

        
    }

    private void RemoveListeners() {
        foreach( Action<GameState> entry in _enteringEvents ) {
            GameStateManager.INSTANCE.StopListeningStateEnter(entry);
        }
        foreach( Action<GameState> entry in _exitingEvents ) {
            GameStateManager.INSTANCE.StopListeningStateExit(entry);
        }
    }
}
