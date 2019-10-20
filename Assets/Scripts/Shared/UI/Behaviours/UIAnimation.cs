using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "UIAnimation", menuName = "Scriptable Objects/UIAnimation", order = 0)]
public class UIAnimation : ScriptableObject
{
    public AnimationCurve curve;
    public float length;
    private AnimationCurve _curveReverse;

    public AnimationCurve curveReverse {
        get {
            if (_curveReverse == null || _curveReverse[0].value != curve[curve.length - 1].value) {
                Keyframe[] keys = curve.keys;
                Array.Reverse(keys);
                _curveReverse = new AnimationCurve(keys);
            }

            return _curveReverse;
        }
    }
}
