using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandoutEnemy : MinigameBehaviour
{
    static bool running = false;
    
    public float rotationRange = 90;
    public float rotationSpeed = 5;
    public int rotationDirection = 1;
    GameObject player;
    float currentRotation;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        currentRotation = 0;
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

    // Update is called once per frame
    void Update()
    {
        if (running) {
            if (rotationDirection == 1) {
                transform.rotation = Quaternion.Euler(Vector3.forward * rotationSpeed * Time.deltaTime + transform.rotation.eulerAngles);
                currentRotation += rotationSpeed * Time.deltaTime;
            } else {
                transform.rotation = Quaternion.Euler(Vector3.forward * -rotationSpeed * Time.deltaTime + transform.rotation.eulerAngles);
                
                currentRotation -= rotationSpeed * Time.deltaTime;
            }
            if (currentRotation > rotationRange / 2) {
                rotationDirection = -1;
            } else if (currentRotation < -rotationRange / 2) {
                rotationDirection = 1;
            }
        }
    }
}
