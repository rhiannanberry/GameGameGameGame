using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2DTopDown : Movable
{
    private Vector3 moveDirection;

    override protected void ReadInput() {
        moveDirection = new Vector3( Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);

    }

    override protected void ClearInput() {
        moveDirection = Vector3.zero;
    }

    override protected void Tick() {
        transform.position += moveDirection * movableSettings.MoveSpeed * Time.deltaTime;
    }
}
