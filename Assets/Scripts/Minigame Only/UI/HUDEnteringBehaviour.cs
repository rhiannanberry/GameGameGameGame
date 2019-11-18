using UnityEngine;
using TMPro;

public class HUDEnteringBehaviour : EnteringBehaviour
{
    private HUDDetails _details;

    protected override void Start() {
        base.Start();
        _details = GetComponent<HUDDetails>();
    }


    protected override void OnStateEnter() {
        _details.InitializeTimer();
        _details.UpdateLives();
    }
    
    protected override void OnStateExit() {
    }
}
