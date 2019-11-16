using System;
using UnityEngine;
using UnityEditor;

using static GameState;

public class GameStateManager : MonoBehaviour
{
    private GameState _state;
    private ListenerDictionary<GameState> _enterStateListeners; 
    private ListenerDictionary<GameState> _exitStateListeners; 

    private Animator _animator;

    public string STATE {get { return _state.ToString(); }}

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

    public void OnDrawGizmos() {
        string s = EditorApplication.isPlayingOrWillChangePlaymode ? STATE : "NONE";
        UnityEditor.Handles.BeginGUI();
        GUI.color = Color.white;
        
        GUIContent label = new GUIContent("GameState: ");
        GUIContent st = new GUIContent(s);

        Vector2 labelSize = GUI.skin.label.CalcSize(label);
        Vector2 stSize = GUI.skin.label.CalcSize(st);

        GUI.Label(new Rect(5,5,labelSize.x, labelSize.y), "GameState: ");
        GUI.Label(new Rect(10+labelSize.x,5,stSize.x, stSize.y), s);
        //Vector2 size = GUI.skin.label.CalcSize(new GUIContent("ssttst hshd"));
        //GUI.Label(new Rect(0,0,size.x,size.y), "kdkdkd");
        UnityEditor.Handles.EndGUI();
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
