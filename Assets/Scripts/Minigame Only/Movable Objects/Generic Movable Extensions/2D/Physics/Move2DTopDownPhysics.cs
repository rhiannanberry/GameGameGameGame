using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2DTopDownPhysics : Movable
{
   private Vector2 moveDirection;
   private Rigidbody2D rb;

   void Start() {
       rb = GetComponent<Rigidbody2D>();
   }

    override protected void ReadInput() {
        moveDirection = new Vector2( Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    }

    override protected void ClearInput() {
        moveDirection = Vector2.zero;
    }

    override protected void Tick() {
        rb.AddForce(moveDirection * movableSettings.MoveSpeed);
    }
}
