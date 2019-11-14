using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movable : MinigameBehaviour
{
    [SerializeField] protected MovableSettings movableSettings;
    public static bool canMove = false;

    protected override void OnStateEnter() {
        canMove = true;
    }

    protected override void OnStateExit() {
        canMove = false;
    }

    private void FixedUpdate() {
        //TODO: Disable read input before and after game
        Tick();
    }

    private void Update() {
        if (canMove) {
            ReadInput();
        } else {
            ClearInput();
        }
    }

    protected abstract void ReadInput();
    protected abstract void ClearInput();
    protected abstract void Tick();

}
