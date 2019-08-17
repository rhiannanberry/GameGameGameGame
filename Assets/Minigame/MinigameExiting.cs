using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MinigameExiting : ExitingBehaviour
{
    [SerializeField] private float _exitTime = 5f;
    [SerializeField] private TransitionData _transitionData;

    [SerializeField] private GameObject _canvas;
    [SerializeField] private RectTransform _backgroundPanel;

    private float _time = 0f;

    protected override void OnStateEnter() {
        Debug.Log("EXITING");
        PersistentDataManager.INSTANCE.SaveMinigameMasterList();
        StartCoroutine(EXIT());
    }

    protected override void OnStateExit() {
        
    }

    IEnumerator EXIT() {
        _canvas.SetActive(true);
        yield return new WaitForSeconds(_exitTime);
        StartCoroutine(ExitTransition());
    }

    IEnumerator ExitTransition() {
        while (_time <= _transitionData.Length) {

            float scaledTime01 =  _transitionData.GetEased(_time/_transitionData.Length);
            _backgroundPanel.anchorMax = new Vector2( scaledTime01, _backgroundPanel.anchorMax.y );
            _time += Time.deltaTime;
            yield return null;
        }
        

        SceneManager.LoadScene(PersistentDataManager.run.NextScene());
    }
}
