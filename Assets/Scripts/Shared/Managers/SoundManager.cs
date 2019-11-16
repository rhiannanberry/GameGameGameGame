using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] SFXSources;

    public AudioClip MainMusic, CustomMusic;
    public AudioSource MusicSource;
    public AudioSource MusicSourceSecondary;

    public static SoundManager INSTANCE = null;

    private void Awake() {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("SFX");
        SFXSources = new AudioSource[gos.Length];
        for (int i = 0; i <gos.Length; i++ ) {
            SFXSources[i] = gos[i].GetComponent<AudioSource>();
        }

        if (INSTANCE == null) {
            INSTANCE = this;
        } else if (INSTANCE != this) {
            INSTANCE.SFXSources = SFXSources;
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioSource aus) {
        if (Array.Exists(SFXSources, e => e == aus)) {
            aus.Play();
        }
    }

    public void PlayMusic(AudioClip clip) {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    public void UpdateVolume() {
        float master = PersistentDataManager.playerSettings.MasterVolume;
        float music = master * PersistentDataManager.playerSettings.MusicVolume;
        float sfx = master * PersistentDataManager.playerSettings.SFXVolume;

        foreach (AudioSource s in SFXSources) {
            s.volume = sfx;
        }

        MusicSource.volume = music;
    }
}

