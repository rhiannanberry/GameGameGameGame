using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class Collisions : MonoBehaviour
{
    
    
    
    [HideInInspector]
    public bool useCustom;
    [HideInInspector]
    public Result collisionResult;
    [HideInInspector]
    public UnityEvent customCollisionResults;

    [Tooltip("If no sources, the result will occur with all collisions")]
    public GameObject[] sources;
    
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("col2d");
        if (ValidateCollision(col.gameObject)) {
            ExecuteCollisionResult(col.gameObject);
        }
    }
    void OnCollisionEnter(Collision other) {
        Debug.Log("col");
        if (ValidateCollision(other.gameObject)) {
            ExecuteCollisionResult(other.gameObject);
        }
    }
    
    void OnTriggerEnter(Collider other) {
        Debug.Log("trigger");
        if (ValidateCollision(other.gameObject)) {
            ExecuteCollisionResult(other.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger2d");
        if (ValidateCollision(other.gameObject)) {
            ExecuteCollisionResult(other.gameObject);
        }
    }

    bool ValidateCollision(GameObject colObj) {
        return (sources.Length == 0 || Array.Find(sources, element => element == colObj));
    }

    void ExecuteCollisionResult(GameObject other) {
        switch(collisionResult) {
            case Result.Win:
                PersistentDataManager.run.GameWon();
            break;
            case Result.Lose:
                PersistentDataManager.run.GameLost();
            break;
            case Result.Hurt:
                //TODO: idk
            break;
        }
        if (useCustom) {
            customCollisionResults.Invoke();
        }
    }
}

public enum Result{Win, Lose, Hurt, Custom};
