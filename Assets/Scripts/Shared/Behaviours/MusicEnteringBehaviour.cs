using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicEnteringBehaviour : EnteringBehaviour
{
    protected override void OnStateEnter() {
        MusicManager.INSTANCE.CheckTransition();
    }

    protected override void OnStateExit() {
    }
}
