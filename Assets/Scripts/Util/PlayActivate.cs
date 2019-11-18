using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayActivate : MonoBehaviour
{
    public List<GameObject> ensureActive;
    public List<GameObject> ensureInactive;
    void Awake() {
        foreach(var g in ensureActive) {
            g.SetActive(true);
        }
        foreach(var g in ensureInactive) {
            g.SetActive(false);
        }
    }
}
