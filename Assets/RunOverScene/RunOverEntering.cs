using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOverEntering : EnteringBehaviour
{
    protected override void OnStateEnter() {
        //TODO:  add visuals n stuff
        GameStateManager.INSTANCE.TriggerStateChange(GameState.INSCENE);
    }

    protected override void OnStateExit() {

    }
}
