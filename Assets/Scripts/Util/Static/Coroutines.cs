using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutines
{
    public static IEnumerator InterpolateCurve(AnimationCurve curve, float duration, System.Action<float> position) {
        float currentTime = 0f;
        float curveTimeLength = curve[curve.length - 1].time;
        
        while (currentTime <= duration) {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(currentTime / duration);
            float curveTime = percent*curveTimeLength;

            position(curve.Evaluate(curveTime));
            yield return null;
        }
        position(curve.Evaluate(curveTimeLength));
    }

    public static IEnumerator InterpolateCurve(AnimationCurve curve, System.Action<float> position) {
        float currentTime = 0f;
        float curveTimeLength = curve[curve.length - 1].time;
        
        while (currentTime <= curveTimeLength) {
            currentTime += Time.deltaTime;
            position(curve.Evaluate(currentTime));
            yield return null;
        }
        position(curve.Evaluate(curveTimeLength));
    }

    public static IEnumerator Count(float duration, System.Action<float> position) {
        float currentTime = 0f;
        
        while (currentTime <= duration) {
            currentTime += Time.deltaTime;
            position(currentTime);
            yield return null;
        }
        position(duration);
    }

    public static IEnumerator Lerp(float startValue, float endValue, float duration, System.Action<float> position) {
        float currentTime = 0f;
        while (currentTime <= duration) {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(currentTime / duration);
            position(startValue + percent * (endValue - startValue));
            yield return null;
        }
        position(endValue);
    }

    public static IEnumerator WaitTime(float seconds, System.Action<bool> complete) {
        complete(false);
        yield return new WaitForSeconds(seconds);
        complete(true);
        
    }
}


