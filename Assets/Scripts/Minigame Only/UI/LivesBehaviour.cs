using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LivesBehaviour : MinigameBehaviour
{
    [SerializeField] private GameObject _lifePrefab = null;

    protected override void Start() {
        base.Start();
        UpdateLives();
    }

    protected override void OnStateEnter() {}
    
    protected override void OnStateExit() {
        UpdateLives();
    }

    private void UpdateLives() {
        foreach (Transform child in GetComponentsInChildren<Transform>()) {
            if (child == transform) continue;
            GameObject.Destroy(child.gameObject);
        }
        for (int i=0; i < PersistentDataManager.run.Lives; i++) {
            Instantiate(_lifePrefab, transform);
        }
    }
}
