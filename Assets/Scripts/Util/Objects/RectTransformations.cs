using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformations
{
    public Vector2 anchorMin;
    public Vector2 anchorMax;
    public Vector2 localScale;

    public RectTransformations(RectTransform rt) {
        anchorMin = rt.anchorMin;
        anchorMax = rt.anchorMax;
        localScale = rt.localScale;
    }
}
