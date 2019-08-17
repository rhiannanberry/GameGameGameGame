using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ease { LINEAR, IN, OUT, INOUT, CUSTOM};

public static class EaseExtensions {
    public static float GetEased(this Ease ease, float t) {
        switch( ease ) {
            case Ease.IN:       return t*t;
            case Ease.OUT:      return (2 - t) * t;
            case Ease.INOUT:    return -t*t*(2*t-3);
            default:            return t;
        }
    }
}