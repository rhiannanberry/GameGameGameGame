using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Transition Data", menuName = "ScriptableObjects/Transition Data", order = 2)]
public class TransitionData : ScriptableObject
{
    [SerializeField] private Ease easingType;
    [SerializeField] private AnimationCurve customEasingCurve;
    [SerializeField] [Range(0f, 10f)] private float transitionLength;

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
}
