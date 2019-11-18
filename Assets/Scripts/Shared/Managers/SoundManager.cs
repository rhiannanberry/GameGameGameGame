using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager INSTANCE;
    public AudioFile[] audioFiles;

    private void Awake() {
        INSTANCE = this;
        foreach (var a in audioFiles) {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.audioClip;
            a.source.volume = a.volume;
            a.source.loop = false;
            a.source.playOnAwake = false;
        }
    }

    public void PlaySound(string name) {
        AudioFile m = Find(name);
        if (m != null) m.source.Play();
    }
    public void StopSound(string name) {
        AudioFile m = Find(name);
        if (m != null) m.source.Stop();
    }
    public void PauseSound(string name) {
        AudioFile m = Find(name);
        if (m != null) m.source.Pause();
    }
    public void UnPauseSound(string name) {
        AudioFile m = Find(name);
        if (m != null) m.source.UnPause();
    }

    public static void _PlaySound(string name) {
        AudioFile m = Find(name);
        if (m != null) m.source.Play();
    }


    public static void _StopSound(string name) {
        AudioFile m = Find(name);
        if (m != null) m.source.Stop();
    }

    public static void _PauseSound(string name) {
        AudioFile m = Find(name);
        if (m != null) m.source.Pause();
    }

    public static void _UnPauseSound(string name) {
        AudioFile m = Find(name);
        if (m != null) m.source.UnPause();
    }

    public static void _PauseAll() {
        foreach(var m in INSTANCE.audioFiles) {
            m.source.Pause();
        }
    }

    public static void _UnPauseAll() {
        foreach(var m in INSTANCE.audioFiles) {
            m.source.UnPause();
        }
    }

    public static void _UpdateVolume() {
        if (INSTANCE == null) {
            Debug.LogWarning("Instance of SoundManager does not exist");
            return;
        }

        float master = PersistentDataManager.playerSettings.MasterVolume;
        float sfx = master * PersistentDataManager.playerSettings.SFXVolume;
        foreach (var m in INSTANCE.audioFiles) {
            m.source.volume = sfx;
        }
    }

    private static AudioFile Find(string name) {
        AudioFile m = Array.Find(INSTANCE.audioFiles, a => a.audioName == name);

        if (m == null) {
            Debug.LogError("Sound name " + name + " not found!" );
        }

        return m;
    }
}

