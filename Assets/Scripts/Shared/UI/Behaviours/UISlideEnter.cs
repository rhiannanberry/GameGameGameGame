using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISlideEnter : EnteringBehaviour
{
    [Range(0f,10f)]
    [SerializeField] private float _timeLength = 2f;
    [SerializeField] private AnimationCurve _curve = null;
    [SerializeField] private RectTransform _rt = null;

    protected override void OnStateEnter() {
        _rt = GetComponent<RectTransform>();

        var expectedValue = _curve[_curve.length - 1].value;

        StartCoroutine(Coroutines.InterpolateCurve(_curve, _timeLength, (value) => {

            if( value == expectedValue ) {
                gameObject.SetActive(false);
                return;
            }
            _rt.anchorMin = new Vector2( 2f*value - 1f, _rt.anchorMin.y );
            _rt.anchorMax = new Vector2( 2f*value,      _rt.anchorMax.y );

        }));
    }

    protected override void OnStateExit() {
        
    }
}
