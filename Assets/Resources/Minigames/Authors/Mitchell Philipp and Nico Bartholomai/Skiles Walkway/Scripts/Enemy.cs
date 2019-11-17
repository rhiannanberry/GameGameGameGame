using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MinigameBehaviour
{
    static bool running = false;
    
    GameObject player;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
    }

    protected override void OnStateEnter() {
        if (!running) running = true;
    }

    protected override void OnStateExit() {
        if (running) running = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == player && running) {
            PersistentDataManager.RUN.GameLost();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Collid");
        if (other.gameObject == player && running) {
            PersistentDataManager.RUN.GameLost();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (running) {
            
        }
    }
}
