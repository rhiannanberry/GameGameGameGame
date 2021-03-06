﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Timers 
{
    public static float MINIGAME { get; set;}
    public static float TRANSITIONS { get; set;}
    public static float PAUSE { get; set;}
    public static void ResetTimers() {
        MINIGAME = TRANSITIONS = PAUSE = 0;
    }

    public static void PrintTimers() {
        Debug.Log("Minigame Time: " + MINIGAME);
        Debug.Log("Transitions Time: " + TRANSITIONS);
        Debug.Log("Pause Time: " + PAUSE);
    }

    public static string MINIGAME_STR() {
        return string.Format("{0:N1}", MINIGAME);
    }

    public static string PAUSE_STR() {
        return string.Format("{0:N1}", PAUSE);
    }
}
