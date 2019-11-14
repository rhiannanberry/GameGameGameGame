using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigameEntering : EnteringBehaviour
{

    [Header("Master Transition")]
    [SerializeField] private float _enterTime = 4f;
    [SerializeField] private TextMeshProUGUI _title = null;
    [SerializeField] private TextMeshProUGUI _author = null;
    [SerializeField] private TextMeshProUGUI _description = null;
    [SerializeField] private TextMeshProUGUI _timeLimit = null;

    [Header("Background Transition")]
    [SerializeField] private TransitionData _bgTransition = null;
    [SerializeField] private RectTransform _bgRect = null;

    [Header("Minigame Details Transition")]
    [SerializeField] private TransitionData _textTransition = null;
    [SerializeField] private RectTransform _textRect = null;

    protected override void OnStateEnter() {
        Minigame m = PersistentDataManager.run.CurrentGame;
        _title.text = m.Name;
        _author.text = "Made by: " + m.Author;
        _description.text = m.Description;
        _timeLimit.text = m.TimeLimit + " Seconds";
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
