using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPausingBehaviour : PauseBehaviour
{
    protected override void OnStateEnter() {
        SoundManager._PauseAll();
    }

    protected override void OnStateExit() {
        SoundManager._UnPauseAll();
    }
}
