using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Transition Data", menuName = "ScriptableObjects/Transition Data", order = 2)]
public class TransitionData : ScriptableObject
{
    [SerializeField] private Ease easingType = Ease.LINEAR;
    [SerializeField] private AnimationCurve customEasingCurve = null;
    [SerializeField] [Range(0f, 10f)] private float transitionLength = 2f;

    public float Length { get {return transitionLength; }}

    public float GetEased(float t) {
        if (easingType == Ease.CUSTOM) {
            return customEasingCurve.Evaluate(t);
        }

        return easingType.GetEased(t);
    }

    public float GetEasedMax() {
        if (easingType == Ease.CUSTOM) {
            return customEasingCurve.Evaluate(1f);
        }
        return easingType.GetEased(1f);
    }

    public IEnumerator Transition(System.Action<float> t, System.Action<bool> complete ) {
        float _time = 0f;
        
        complete( false );
        
        while ( _time <= transitionLength ) {
            t( GetEased( _time / transitionLength ) );
            _time += Time.deltaTime;
            yield return null;
        }
        
        t( GetEasedMax() );
        
        complete( true );
    }

    public IEnumerator ReverseTransition(System.Action<float> t, System.Action<bool> complete) {
        float _time = transitionLength;
        
        complete( false );
        
        while ( _time >= transitionLength ) {
            t( GetEased( _time / transitionLength ) );
            _time -= Time.deltaTime;
            yield return null;
        }
        
        t( 0f );
        
        complete( true );
    }

    public IEnumerator StartTransitionSlide( params RectTransform[] rects ) {
        float _time = 0f;

        while (_time <= transitionLength) {
            float scaledTime = GetEased( _time / transitionLength);
            foreach (RectTransform rect in rects) {
                rect.anchorMin = new Vector2( scaledTime - 1,   rect.anchorMin.y);
                rect.anchorMax = new Vector2( scaledTime ,      rect.anchorMax.y);
            }
            _time += Time.deltaTime;
            yield return null;
        }

        float maxEase = GetEasedMax();

        foreach (RectTransform rect in rects) {
            rect.anchorMin = new Vector2( maxEase - 1   , rect.anchorMin.y);
            rect.anchorMax = new Vector2( maxEase       , rect.anchorMax.y);
        }

    }

}
