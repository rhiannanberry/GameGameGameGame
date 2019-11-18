using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonFunctions : MinigameBehaviour
{
    private Button _btn;
    protected override void Start() {
        base.Start();
        _btn = GetComponent<Button>();
        _btn.interactable = false;
    }

    protected override void OnStateEnter() {
        _btn.interactable = true;
    }

    protected override void OnStateExit() {
        _btn.interactable = false;
    }

    public void GameWon() {
        PersistentDataManager.RUN.GameWon();
    }

    public void GameLost() {
        PersistentDataManager.RUN.GameLost();
    }

    public void IncreaseScore(int amt) {
        PersistentDataManager.RUN.CurrentGame.CurrentScore += amt;
    }
}
