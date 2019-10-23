using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move2DSideViewPhysics : Movable
{
    [SerializeField] private bool canJump;

    [Range(1,99999)]
    [SerializeField] private int numberOfJumps = 1;
    private bool isGrounded;
    private int jumpNumber;
    private float jump;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }

    override protected void ReadInput() {
        moveDirection = new Vector2( Input.GetAxisRaw("Horizontal"), 0);
        
        if (canJump && Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpNumber < numberOfJumps )) {
            isGrounded = false;
            ++jumpNumber;
            jump = 1f;
        }
    }

    override protected void ClearInput() {
        moveDirection = Vector2.zero;
        jump = 0f;
    }

    override protected void Tick() {
        rb.AddForce(moveDirection * movableSettings.MoveSpeed);
        rb.AddForce(new Vector2(0,1) * jump * movableSettings.JumpSpeed, ForceMode2D.Impulse);
        jump = 0f;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (rb.velocity.y <= 0) {
            isGrounded = true;
            jumpNumber = 0;
        }
    }
}
