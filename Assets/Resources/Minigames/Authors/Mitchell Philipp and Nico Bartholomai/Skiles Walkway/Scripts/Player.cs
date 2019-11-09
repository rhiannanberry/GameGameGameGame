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
    int direction = 0;

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
            bool walking = false;
            Vector2 newForce = new Vector2(0,0);
            if (Input.GetKey(KeyCode.UpArrow)) {
                newForce += Vector2.up;
                walking = true;
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                newForce += Vector2.right;
                walking = true;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                newForce += Vector2.down;
                walking = true;
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                newForce += Vector2.left;
                walking = true;
            }
            newForce.Normalize();
            newForce *= moveForce * Time.deltaTime;
            r.AddForce(newForce, ForceMode2D.Impulse);
            if (newForce.x > 0) {
                direction = 0;
            } else if (newForce.x < 0) {
                direction = 2;
            } else if (newForce.y > 0) {
                direction = 1;
            } else if (newForce.y < 0) {
                direction = 3;
            }
            Debug.Log(direction);
            a.SetInteger("direction", direction);
            a.SetBool("walking", walking);
        }
    }
}
