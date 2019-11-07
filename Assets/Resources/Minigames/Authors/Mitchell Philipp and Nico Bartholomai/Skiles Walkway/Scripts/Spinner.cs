using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MinigameBehaviour
{
    public float spinSpeed = 5;
    static bool running = false;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();    
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
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * spinSpeed));
        }
    }
}
