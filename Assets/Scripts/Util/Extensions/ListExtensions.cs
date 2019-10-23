using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ListExtensions
{
    public static System.Random rnd = new System.Random();

    public static void Shuffle<T>(this IList<T> list) {
        int n = list.Count;
        while (n > 1) {
            int k = (rnd.Next(0, n) % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
