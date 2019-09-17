using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MinigameBehaviour
{

    Rigidbody2D r;
    bool running = false;
    public float moveForce = 5;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        r = GetComponent<Rigidbody2D>();
    }

    protected override void OnStateEnter() {
        running = true;
    }

    protected override void OnStateExit() {
        running = false;
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
        }
    }
}
