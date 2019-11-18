using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownFlyer : MinigameBehaviour
{
    public static bool running = false;
    public GameObject player;
    public float speed = 3;
    Vector3 directionVector;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        Destroy(this.gameObject, 10);
        float direction = transform.rotation.eulerAngles.z * (float)Math.PI / 180 - (float)Math.PI / 2;
        directionVector = new Vector3((float)Math.Cos(direction), (float)Math.Sin(direction));
    }

    protected override void OnStateEnter() {
        if (!running) running = true;
    }

    protected override void OnStateExit() {
        if (running) running = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("hit!");
        if (other.gameObject == player && running) {
            PersistentDataManager.RUN.GameLost();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (running) {
            transform.position += directionVector * speed * Time.deltaTime;
        }
    }
}
