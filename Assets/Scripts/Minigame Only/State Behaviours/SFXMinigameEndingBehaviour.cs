using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXMinigameEndingBehaviour : ExitingBehaviour
{
    public AudioClip winSound, loseSound;
    private AudioSource sfx;

    protected override void Start() {
        base.Start();
        sfx = GetComponent<AudioSource>();
    }

    protected override void OnStateEnter() {
        if (PersistentDataManager.RUN.WonGame && winSound != null) {
            sfx.clip = winSound;
            sfx.Play();
        } else if (loseSound != null) {
            sfx.clip = loseSound;
            sfx.Play();
        }

    }

    protected override void OnStateExit() {
        
    }
}
