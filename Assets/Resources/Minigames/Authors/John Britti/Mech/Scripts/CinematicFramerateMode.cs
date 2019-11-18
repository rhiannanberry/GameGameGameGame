using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicFramerateMode : MonoBehaviour {
    private int prevFramerate;
    [SerializeField] private int targetFramerate = 24;
    // Start is called before the first frame update
    void Start() {
        if (Application.targetFrameRate > targetFramerate) {
            QualitySettings.vSyncCount = 0;
            prevFramerate = Application.targetFrameRate;
            Application.targetFrameRate = targetFramerate;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Application.targetFrameRate != targetFramerate)
            Application.targetFrameRate = targetFramerate;
    }
}