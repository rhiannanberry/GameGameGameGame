using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameEnteringUI : EnteringBehaviour
{
    [SerializeField] private TransitionData _transitionData = null;
    private RectTransform _rectTransform;

    private float _time = 0;

    protected override void OnStateEnter() {
        _rectTransform = GetComponent<RectTransform>();
        StartCoroutine(Slide());
    }
    protected override void OnStateExit() {
        gameObject.SetActive(false);
    }

    private IEnumerator Slide() {
        while (_time <= _transitionData.Length) {

            float scaledTime01 = _transitionData.GetEased( _time/_transitionData.Length );
            _rectTransform.anchorMin = new Vector2( scaledTime01 - 1,   _rectTransform.anchorMin.y );
            _rectTransform.anchorMax = new Vector2( scaledTime01,       _rectTransform.anchorMax.y );
            _time += Time.deltaTime;
            yield return null;

        }

        float maxEase = _transitionData.GetEasedMax();
        
        _rectTransform.anchorMin = new Vector2( maxEase - 1,   _rectTransform.anchorMin.y );
        _rectTransform.anchorMax = new Vector2( maxEase,       _rectTransform.anchorMax.y );
    }
}
