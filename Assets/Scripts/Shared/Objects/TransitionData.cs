using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Transition Data", menuName = "Scriptable Objects/Transition Data", order = 2)]
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

    public IEnumerator StartTransitionSlide( RectTransform rect, bool transitionIn, Vector2 direction, System.Action<bool> complete ) {
        complete(false);
        Vector2 _anchorMin = rect.anchorMin;
        Vector2 _anchorMax = rect.anchorMax;
        float _offset = transitionIn ? 0 : 1;
        float _time = 0f;
        direction = direction.normalized;

        while (_time <= transitionLength) {
            float scaledTime = _offset + GetEased( _time / transitionLength);
            rect.anchorMin = _anchorMin + direction * scaledTime;
            rect.anchorMax = _anchorMax + direction * scaledTime;
            //rect.anchorMin = new Vector2( _anchorMin.x + direction.x * scaledTime,   rect.anchorMin.y);
            //rect.anchorMax = new Vector2( _anchorMax.x + direction.x * scaledTime ,      rect.anchorMax.y);

            _time += Time.deltaTime;
            yield return null;
        }

        float maxEase = GetEasedMax();


        rect.anchorMin = _anchorMin + direction * (_offset + maxEase);
        rect.anchorMax = _anchorMax + direction * (_offset + maxEase);
        //rect.anchorMin = new Vector2( _anchorMin.x + direction.x * (_offset + maxEase)   , rect.anchorMin.y);
        //rect.anchorMax = new Vector2( _anchorMax.x + direction.x * (_offset + maxEase)       , rect.anchorMax.y);
        
        complete(true);
    }

    public IEnumerator StartTransitionScale( RectTransform rect, bool transitionIn, System.Action<bool> complete ) {
        complete(false);
        float _offset = transitionIn ? 0 : 1;
        float _scale = transitionIn ? 1 : -1;
        float _time = 0f;

        while (_time <= transitionLength) {
            float scaledTime = _offset + _scale*GetEased( _time / transitionLength);

            rect.transform.localScale = Vector3.one * scaledTime;

            _time += Time.deltaTime;
            yield return null;
        }

        rect.transform.localScale = Vector3.one * (_offset + _scale*GetEasedMax());
        
        complete(true);
    }

}
