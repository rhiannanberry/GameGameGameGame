using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDExitingBehaviour : ExitingBehaviour
{
    private HUDDetails _details;

    protected override void Start() {
        base.Start();
        _details = GetComponent<HUDDetails>();
    }

    protected override void OnStateEnter() {
        _details.UpdateLives();
    }

    protected override void OnStateExit() {}

}
