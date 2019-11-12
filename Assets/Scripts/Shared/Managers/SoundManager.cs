using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource SFXSource;
    public AudioSource MusicSource;

    public static SoundManager INSTANCE = null;

    private void Awake() {
        if (INSTANCE == null) {
            INSTANCE = this;
        } else if (INSTANCE != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioClip clip){
        SFXSource.clip = clip;
        SFXSource.Play();
    }

    public void PlayMusic(AudioClip clip) {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    public void UpdateVolume() {
        float master = PersistentDataManager.playerSettings.MasterVolume;
        float music = master * PersistentDataManager.playerSettings.MusicVolume;
        float sfx = master * PersistentDataManager.playerSettings.SFXVolume;

        SFXSource.volume = sfx;
        MusicSource.volume = music;
    }
}

