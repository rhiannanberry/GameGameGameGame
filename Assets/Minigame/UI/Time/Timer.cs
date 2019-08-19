using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : GameStateBehaviourNew
{
    private bool _timerActive = false;
    private TextMeshProUGUI _timerUI;

    public static float time = 0;

    public void OnStateEnterENTERING() {
        _timerUI = GetComponent<TextMeshProUGUI>();
        time = PersistentDataManager.run.CurrentGame.TimeLimit;
        UpdateTimerUI();
    }
    

    public void OnStateEnterINGAME() {
        _timerActive = true;
    }

    public void OnStateExitINGAME() {
        _timerActive = false;
    }

    private void Update() {
        if (_timerActive == false) 
            return;
        
        time -= Time.deltaTime;
        time = Mathf.Max(0, time);
        UpdateTimerUI();
        if (time == 0) {
            _timerActive = false;
            PersistentDataManager.run.GameLost();
        }
        
    }

    private void UpdateTimerUI() {
        _timerUI.text = string.Format("{0:N1}", time);
    }
}
