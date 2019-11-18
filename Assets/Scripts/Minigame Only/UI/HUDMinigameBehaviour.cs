using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDMinigameBehaviour : MinigameBehaviour
{
    private HUDDetails _details;

    private bool _inMinigame = false;

    protected override void Start() {
        base.Start();
        _details = GetComponent<HUDDetails>();
    }

    protected override void OnStateEnter() {
        _inMinigame = true;
    }

    protected override void OnStateExit() {
        _inMinigame = false;
        _details.UpdateLives();
    }

    void Update() {
        if (!_inMinigame) return;
        _details.UpdateTimer();
    }
}
