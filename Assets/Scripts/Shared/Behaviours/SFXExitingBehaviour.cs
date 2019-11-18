using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXExitingBehaviour : ExitingBehaviour
{
    public string[] playOnWin;
    public string[] playOnLose;
    public string[] playAlways;

    protected override void OnStateEnter() {
        PlayClips(playAlways);
        if (PersistentDataManager.InMinigame) {
            if (PersistentDataManager.RUN.WonGame) {
                PlayClips(playOnWin);
            } else {
                PlayClips(playOnLose);
            }
        }
    }

    private void PlayClips(string[] clipNames) {
        if (clipNames.Length == 0) {
            return;
        }
        foreach(string c in clipNames) {
            SoundManager._PlaySound(c);
        }
    }

    protected override void OnStateExit() {}
}
