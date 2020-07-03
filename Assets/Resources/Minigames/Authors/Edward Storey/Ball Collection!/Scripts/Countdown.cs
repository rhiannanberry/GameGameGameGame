using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MinigameBehaviour
{
    private bool _timerActive = false;
    private TextMeshProUGUI _timerUI;

    public static float time = 0;

    private bool _playedOnce = false;

    protected override void Start() {
        base.Start();
        _timerUI = GetComponent<TextMeshProUGUI>();
        time = 4;
        UpdateTimerUI();
    }

    protected override void OnStateEnter() {
        _timerActive = true;
        _timerUI.enabled = true;
        if (!_playedOnce) {
            SoundManager._PlaySound("countdown");
            _playedOnce = true;

        }
        
    }

    protected override void OnStateExit() {
        _timerActive = false;
    }

    private void Update() {
        if (_timerActive == false)
            return;
        if (time > 0)
            HUDDetails.time = PersistentDataManager.RUN.CurrentGame.TimeLimit;
            //TimerBehaviour.time = PersistentDataManager.RUN.CurrentGame.TimeLimit;
        time -= Time.deltaTime;
        UpdateTimerUI();
    }

    private void UpdateTimerUI() {
        if ((int) time == 3) {
            _timerUI.text = string.Format("{0:N1}", "Three");
        }
        if ((int) time == 2) {
            _timerUI.text = string.Format("{0:N1}", "Two");
        }
        if ((int) time == 1) {
            _timerUI.text = string.Format("{0:N1}", "One");
        }
        if ((int) time <= 0) {
            _timerUI.text = string.Format("{0:N1}", "go!");
            if ((int) time == -2) {
                _timerUI.enabled = false;
            }
        }
    }
}
