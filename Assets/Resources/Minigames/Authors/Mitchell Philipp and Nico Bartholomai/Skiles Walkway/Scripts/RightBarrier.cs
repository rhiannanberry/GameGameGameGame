using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBarrier : MinigameBehaviour
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

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject == player && running) {
            PersistentDataManager.run.GameWon();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
