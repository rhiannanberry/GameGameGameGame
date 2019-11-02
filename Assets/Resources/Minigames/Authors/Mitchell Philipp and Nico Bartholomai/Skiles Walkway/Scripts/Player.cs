using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MinigameBehaviour
{

    Rigidbody2D r;
    Animator a;
    static bool running = false;
    public float moveForce = 5;
    public float walkingThreshold = 1;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        r = GetComponent<Rigidbody2D>();
        a = GetComponent<Animator>();
    }

    protected override void OnStateEnter() {
        if (!running) running = true;
    }

    protected override void OnStateExit() {
        if (running) running = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (running) {
            Vector2 newForce = new Vector2(0,0);
            if (Input.GetKey(KeyCode.UpArrow)) {
                newForce += Vector2.up;
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                newForce += Vector2.right;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                newForce += Vector2.down;
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                newForce += Vector2.left;
            }
            newForce.Normalize();
            newForce *= moveForce * Time.deltaTime;
            r.AddForce(newForce, ForceMode2D.Impulse);
            Vector2 velocity;
            velocity = r.velocity;
            int direction = (int) (Math.Atan(velocity.y / velocity.x) * ((float) (2 / Math.PI)));
            bool walking = Math.Pow(Math.Pow(velocity.x, 2) + Math.Pow(velocity.y, 2), .5) > walkingThreshold;
            a.SetInteger("direction", direction);
            a.SetBool("walking", walking);
        }
    }
}
