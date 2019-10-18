using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class RectTransformExtensions 
{
    public static void AnchorLeft(this RectTransform rt, bool centerY) {
        float yMin = centerY ? .5f : rt.anchorMin.y;
        float yMax = centerY ? .5f : rt.anchorMax.y;

        rt.anchorMin = new Vector2(0, yMin);
        rt.anchorMax = new Vector2(0, yMax);
    }

    public static void AnchorRight(this RectTransform rt, bool centerY) {
        float yMin = centerY ? .5f : rt.anchorMin.y;
        float yMax = centerY ? .5f : rt.anchorMax.y;

        rt.anchorMin = new Vector2(1, yMin);
        rt.anchorMax = new Vector2(1, yMax);
    }

    public static void AnchorTop(this RectTransform rt, bool centerX) {
        float xMin = centerX ? .5f : rt.anchorMin.x;
        float xMax = centerX ? .5f : rt.anchorMax.x;

        rt.anchorMin = new Vector2(xMin, 1);
        rt.anchorMax = new Vector2(xMax, 1);
    }

    public static void AnchorBottom(this RectTransform rt, bool centerX) {
        float xMin = centerX ? .5f : rt.anchorMin.x;
        float xMax = centerX ? .5f : rt.anchorMax.x;

        rt.anchorMin = new Vector2(xMin, 0);
        rt.anchorMax = new Vector2(xMax, 0);
    }

    public static void AnchorCenterX(this RectTransform rt) {
        rt.anchorMin = new Vector2(.5f, rt.anchorMin.y);
        rt.anchorMax = new Vector2(.5f, rt.anchorMax.y);
    }

    public static void AnchorCenterY(this RectTransform rt) {
        rt.anchorMin = new Vector2(rt.anchorMin.x, .5f);
        rt.anchorMax = new Vector2(rt.anchorMax.x, .5f);
    }

    public static void AnchorCenter(this RectTransform rt) {
        rt.AnchorCenterX();
        rt.AnchorCenterY();
    }

    public static void PivotLeft(this RectTransform rt, bool centerX) {
        float yPivot = centerX ? .5f : rt.pivot.y;
        rt.pivot = new Vector2(0, yPivot);
    }

    public static void PivotRight(this RectTransform rt, bool centerX) {
        float yPivot = centerX ? .5f : rt.pivot.y;
        rt.pivot = new Vector2(1, yPivot);
    }

    public static void PivotTop(this RectTransform rt, bool centerX) {
        float xPivot = centerX ? .5f : rt.pivot.x;
        rt.pivot = new Vector2(xPivot, 1);
    }

    public static void PivotBottom(this RectTransform rt, bool centerX) {
        float xPivot = centerX ? .5f : rt.pivot.x;
        rt.pivot = new Vector2(xPivot, 0);
    }

    public static void PivotCenterX(this RectTransform rt) {
        rt.pivot = new Vector2(.5f, rt.pivot.y);
    }
    public static void PivotCenterY(this RectTransform rt) {
        rt.pivot = new Vector2(rt.pivot.x, .5f);
    }

    public static void PivotCenter(this RectTransform rt) {
        rt.PivotCenterX();
        rt.PivotCenterY();
    }
}
