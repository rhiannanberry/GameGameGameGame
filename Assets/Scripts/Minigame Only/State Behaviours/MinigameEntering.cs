using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigameEntering : EnteringBehaviour
{

    [Header("Master Transition")]
    [SerializeField] private float _exitTime = 4f;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _title = null;
    [SerializeField] private TextMeshProUGUI _author = null;
    [SerializeField] private TextMeshProUGUI _description = null;
    [SerializeField] private TextMeshProUGUI _timeLimit = null;

    protected override void OnStateEnter() {
        Minigame m = PersistentDataManager.RUN.CurrentGame;
        _title.text = m.Name;
        _author.text = "Made by: " + m.Author;
        _description.text = m.Description;
        _timeLimit.text = m.TimeLimit + " Seconds";
        StartCoroutine(ENTER());
    }

    protected override void OnStateExit() {
    }

    private IEnumerator ENTER() {
        yield return new WaitForSeconds(_exitTime);
        GameStateManager.INSTANCE.TriggerStateChange(GameState.INGAME);
    }
}
