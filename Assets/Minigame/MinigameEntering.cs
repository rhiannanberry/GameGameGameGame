using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameEntering : EnteringBehaviour
{
    [SerializeField] private float _enterTime = 4f;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _lives;
    [SerializeField] private TextMeshProUGUI _timeLimit;

    protected override void OnStateEnter() {
        Minigame m = PersistentDataManager.run.CurrentGame;
        _title.text = m.Name;
        _description.text = m.Description;
        _lives.text = "Lives: " + PersistentDataManager.run.Lives;
        _timeLimit.text = "Time Limit: " + m.TimeLimit;
        StartCoroutine(ENTER());
    }

    protected override void OnStateExit() {

    }

    private IEnumerator ENTER() {

        yield return new WaitForSeconds(_enterTime);
        gameObject.SetActive(false);
        GameStateManager.INSTANCE.TriggerStateChange(GameState.INGAME);

    }
}
