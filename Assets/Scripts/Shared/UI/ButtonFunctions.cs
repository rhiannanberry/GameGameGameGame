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
        if (PersistentDataManager.run != null) PersistentDataManager.run.GameWon();
    }

    public void GameLost() {
        if (PersistentDataManager.run != null) PersistentDataManager.run.GameLost();
    }

    public void IncreaseScore(int amt) {
        if (PersistentDataManager.run != null) PersistentDataManager.run.CurrentGame.CurrentScore += amt;
    }
}
