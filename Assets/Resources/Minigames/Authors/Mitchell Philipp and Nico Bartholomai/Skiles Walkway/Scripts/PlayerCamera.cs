using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MinigameBehaviour
{
    static bool running = false;
    GameObject player;
    public GameObject leftBarrier;
    public GameObject rightBarrier;
    public float borderMargin = 10;
    float cameraY;
    float cameraZ;
    
    protected override void OnStateEnter() {
        if (!running) running = true;
    }

    protected override void OnStateExit() {
        if (running) running = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        cameraY = transform.position.y;
        cameraZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (running) {
            float playerX = player.transform.position.x;
            if (playerX < leftBarrier.transform.position.x + borderMargin) {
                transform.position = new Vector3(leftBarrier.transform.position.x + borderMargin, cameraY, cameraZ);
            } else if (playerX > rightBarrier.transform.position.x - borderMargin) {
                transform.position = new Vector3(rightBarrier.transform.position.x - borderMargin, cameraY, cameraZ);
            } else {
                transform.position = new Vector3(playerX, cameraY, cameraZ);
            }
        }
    }
}
