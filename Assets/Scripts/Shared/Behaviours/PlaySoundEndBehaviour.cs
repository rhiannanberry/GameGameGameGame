using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEndBehaviour : ExitingBehaviour
{
    public AudioClip winSound, loseSound;
    public AudioSource sfx;

    protected override void OnStateEnter() {
        if (PersistentDataManager.run.gameWon) {
            sfx.clip = winSound;
        } else {
            sfx.clip = loseSound;
        }

        sfx.Play();
    }

    protected override void OnStateExit() {
        
    }
}
