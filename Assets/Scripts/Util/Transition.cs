using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [Header("Transition Type")]
    public bool fade;

    [Header("Transition")]
    public bool enter = true;
    public bool exit = true;

    [Header("Transition Data")]
    public TransitionDataOLD entering;
    public TransitionDataOLD exiting;
    Image _img;
    RectTransform _rt;
    
    // Start is called before the first frame update
    void Awake()
    {
        _img = GetComponent<Image>();
        _rt = GetComponent<RectTransform>();
        _img.enabled = true;
        _img.SetAlpha(enter ? 1f : 0f);
    }
    void OnEnable() {
        
    }

    void RunTransition(bool isEntering, TransitionDataOLD t) {
        _img.SetAlpha(1f);
        bool triggered = false;
        float expectedValue = t.curve[t.curve.length - 1].value;
        StartCoroutine(Coroutines.InterpolateCurve(t.curve, t.timeLength, (value)=> {
            
            if (fade) {
                _img.SetAlpha(isEntering ? 1f - value : value);
            } else {
                _rt.anchorMin = new Vector2(isEntering ? value : value-1f, _rt.anchorMin.y);
                _rt.anchorMax = new Vector2(isEntering ? value + 1: value, _rt.anchorMax.y);
            }

            if (!triggered && value == expectedValue) {
                triggered = true;
                WaitTimePost(isEntering, t.postDelay);
            }
        }));

    }

    void WaitTimePost(bool isEntering, float time) {
        StartCoroutine(Coroutines.WaitTime(time, (complete) => {
            if (complete) {
                //if (isEntering) EventManager.TriggerEvent("End Enter");
                //if (!isEntering) EventManager.TriggerEvent("End Exit");
            }
        }));
    }

    void WaitTimePre(bool isEntering, TransitionDataOLD t) {
        StartCoroutine(Coroutines.WaitTime(t.preDelay, (complete) => {
            if (complete) RunTransition(isEntering, t);
        }));
    }


    void TransitionTo() {
        WaitTimePre(false, exiting);
    }

    void TransitionFrom() {
        WaitTimePre(true, entering);
    }

}

[System.Serializable]
public class TransitionDataOLD {
    [Range(0, 10)]
    public float preDelay, timeLength, postDelay;
    public AnimationCurve curve;
}
