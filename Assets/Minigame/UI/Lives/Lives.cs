using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GameState;

public class Lives : GameStateBehaviourNew
{
    [SerializeField] private GameObject _lifePrefab;


    public void OnStateEnter(GameState s) {
        if (s == ENTERING) UpdateLives();
    }

    public void OnStateExit(GameState s) {
        if (s == INGAME) UpdateLives();
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
