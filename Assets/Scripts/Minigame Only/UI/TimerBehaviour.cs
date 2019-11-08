using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerBehaviour : MinigameBehaviour
{
    private bool _timerActive = false;
    private TextMeshProUGUI _timerUI;

    public static float time = 0;

    protected override void Start() {
        base.Start();
        _timerUI = GetComponent<TextMeshProUGUI>();
        time = PersistentDataManager.run.CurrentGame.TimeLimit;
        UpdateTimerUI();
    }

    protected override void OnStateEnter() {
        _timerActive = true;
    }

    protected override void OnStateExit() {
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
            PersistentDataManager.run.TimeRanOut();
        }
        
    }

    private void UpdateTimerUI() {
        _timerUI.text = string.Format("{0:N1}", time);
    }
}
