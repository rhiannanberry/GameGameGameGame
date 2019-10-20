using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
    public static void SetX(this Vector3 v, float x) {
        float y = v.y;
        float z = v.z;
        Vector3 newV = new Vector3(x,y,z);
        v = newV;
    }
    public static void SetY(this Vector3 v, float y) {
        float x = v.x;
        float z = v.z;
        Vector3 newV = new Vector3(x,y,z);
        v = newV;
    }
    public static void SetZ(this Vector3 v, float z) {
        float x = v.x;
        float y = v.y;
        Vector3 newV = new Vector3(x,y,z);
        v = newV;
    }

    public static void SetX(this Vector2 v, float x) {
        float y = v.y;
        Vector2 newV = new Vector2(x,y);
        v = newV;
    }
    public static void SetY(this Vector2 v, float y) {
        float x = v.x;
        Vector2 newV = new Vector2(x,y);
        v = newV;       
    }
}
