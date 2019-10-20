using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ImageExtensions
{
    public static void SetAlpha(this Image i, float a) {
        Color temp = i.color;
        temp.a = a;
        i.color = temp;
    }

}
