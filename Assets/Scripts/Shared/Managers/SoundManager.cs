using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager INSTANCE;
    public List<AudioFile> audioFiles;

    private List<AudioFile> externalSFX;

    private void Awake() {
        INSTANCE = this;
        externalSFX = new List<AudioFile>();
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

    public static void _AddExternalSound(AudioFile sound) {
        INSTANCE.audioFiles.Add(sound);
    }

    public static void _PlaySound(string name) {
        AudioFile[] m = FindAll(name);
        if (m.Length == 1) {
            m[0].source.Play();
        } else if (m.Length > 1) {
            AudioFile notPlaying = Array.Find(m, a => !a.source.isPlaying);
            if (notPlaying != null) {
                notPlaying.source.Play();
            } else {
                m[0].source.Play();
            }
        }
    }

    public static void _ModifySource(string name, Func<AudioSource, bool> execute) {
        AudioFile m = Find(name);
        if (m != null) {execute(m.source);}

    }
    public static void _SetPitch(string name, float pitch) {
        AudioFile m = Find(name);
        if (m != null) m.source.pitch = pitch;
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
            m.source.volume = m.volume * sfx;
        }
    }

    private static AudioFile[] FindAll(string name) {
        AudioFile[] m = Array.FindAll(INSTANCE.audioFiles.ToArray(), a => a.audioName == name);
        //if (m == null) m = Array.Find(INSTANCE.externalSFX.ToArray(), a => a.audioName == name);
        if (m.Length == 0) {
            Debug.LogError("Sound name " + name + " not found!" );
        }

        return m;
    }

    private static AudioFile Find(string name) {
        AudioFile m = Array.Find(INSTANCE.audioFiles.ToArray(), a => a.audioName == name);
        //if (m == null) m = Array.Find(INSTANCE.externalSFX.ToArray(), a => a.audioName == name);
        if (m == null) {
            Debug.LogError("Sound name " + name + " not found!" );
        }

        return m;
    }
}

