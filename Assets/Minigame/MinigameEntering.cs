using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigameEntering : EnteringBehaviour
{

    [Header("Master Transition")]
    [SerializeField] private float _enterTime = 4f;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _lives;
    [SerializeField] private TextMeshProUGUI _timeLimit;

    [Header("Background Transition")]
    [SerializeField] private TransitionData _bgTransition;
    [SerializeField] private RectTransform _bgRect;

    [Header("Minigame Details Transition")]
    [SerializeField] private TransitionData _textTransition;
    [SerializeField] private RectTransform _textRect;

    protected override void OnStateEnter() {
        Minigame m = PersistentDataManager.run.CurrentGame;
        _title.text = m.Name;
        _description.text = m.Description;
        _lives.text = "Lives: " + PersistentDataManager.run.Lives;
        _timeLimit.text = "Time Limit: " + m.TimeLimit;
        StartCoroutine(ENTER());
        StartCoroutine(_bgTransition.StartTransitionSlide(_bgRect));
        StartCoroutine(_textTransition.StartTransitionSlide(_textRect));
    }

    protected override void OnStateExit() {
        DeactivateTransitionIn();
    }

    private IEnumerator ENTER() {

        yield return new WaitForSeconds(_enterTime);
        gameObject.SetActive(false);
        GameStateManager.INSTANCE.TriggerStateChange(GameState.INGAME);

    }

    

    private void DeactivateTransitionIn() {
        _bgRect.gameObject.SetActive(false);
        _textRect.gameObject.SetActive(false);
    }
}
