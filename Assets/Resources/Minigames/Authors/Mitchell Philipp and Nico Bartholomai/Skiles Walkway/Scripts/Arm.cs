using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MinigameBehaviour
{
    bool running = false;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    protected override void OnStateEnter() {
        running = true;
    }

    protected override void OnStateExit() {
        running = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == player && running) {
            PersistentDataManager.run.GameLost();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
