using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticThrowerEnemy : MinigameBehaviour
{
    static bool running = false;
    
    GameObject player;
    public GameObject flyerPrefab;

    public float throwInterval = 3;
    public float throwSpeed = 3;
    public string throwSoundName = "throw";
    float throwTimer;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        throwTimer = throwInterval;
    }

    protected override void OnStateEnter() {
        if (!running) {
            running = true;
            ThrownFlyer.running = true;
        }
    }

    protected override void OnStateExit() {
        if (running) { 
            running = false;
            ThrownFlyer.running = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == player && running) {
            PersistentDataManager.RUN.GameLost();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject == player && running) {
            PersistentDataManager.RUN.GameLost();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (running) {
            throwTimer -= Time.deltaTime;
            if (throwTimer <= 0) {
                throwTimer += throwInterval;
                float rotationDegrees = transform.rotation.eulerAngles.z - 90;
                float rotationRadians = rotationDegrees * (float)Math.PI / 180;
                GameObject newFlyer = Instantiate(flyerPrefab, transform.position + new Vector3((float)Math.Cos(rotationRadians), (float)Math.Sin(rotationRadians)), transform.rotation);
                SoundManager._PlaySound(throwSoundName);
                newFlyer.GetComponent<ThrownFlyer>().speed = throwSpeed;
            }
        }
    }
}
