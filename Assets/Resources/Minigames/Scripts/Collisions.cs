﻿using System.Collections;
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
    public string[] tagSources;
    public string[] nameSources;

    [HideInInspector] public GameObject lastCollided;
  

    void OnCollisionEnter2D(Collision2D col)
    {
        if (ValidateCollision(col.gameObject)) {
            ExecuteCollisionResult(col.gameObject);
        }
    }
    void OnCollisionEnter(Collision other) {
        if (ValidateCollision(other.gameObject)) {
            ExecuteCollisionResult(other.gameObject);
        }
    }
    
    void OnTriggerEnter(Collider other) {
        if (ValidateCollision(other.gameObject)) {
            ExecuteCollisionResult(other.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (ValidateCollision(other.gameObject)) {
            ExecuteCollisionResult(other.gameObject);
        }
    }

    bool ValidateCollision(GameObject colObj) {
        bool bothEmpty = (sources.Length == 0 && tagSources.Length == 0 && nameSources.Length == 0);
        bool objMatch = Array.Find(sources, element => element == colObj) != null;
        bool tagMatch = Array.Find(tagSources, element => element == colObj.tag) != null;
        bool nameMatch = Array.Find(nameSources, element => element == colObj.name) != null;
        bool valid = bothEmpty || objMatch || tagMatch || nameMatch;
        if (valid) lastCollided = colObj;
        return valid;
    }

    void ExecuteCollisionResult(GameObject other) {
        if (useCustom) {
            customCollisionResults.Invoke();
        }
        switch(collisionResult) {
            case Result.Win:
                PersistentDataManager.RUN.GameWon();
            break;
            case Result.Lose:
                PersistentDataManager.RUN.GameLost();
            break;
            case Result.Hurt:
                //TODO: idk
            break;
        }
        
    }
}

public enum Result{Win, Lose, Hurt, Custom};
