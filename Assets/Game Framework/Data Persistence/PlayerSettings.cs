using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSettings : System.Object
{
    [SerializeField] private float _masterVolume;
    [SerializeField] private float _sfxVolume;
    [SerializeField] private float _musicVolume;

    //Other things to be added later

    public float MasterVolume { 
        get{ return _masterVolume; }
        set{ _masterVolume = Mathf.Clamp(value, 0f,1f); }
    }

    public float SFXVolume { 
        get{ return _sfxVolume; }
        set{ _sfxVolume = Mathf.Clamp(value, 0f,1f); }
    }

    public float MusicVolume { 
        get{ return _musicVolume; }
        set{ _musicVolume = Mathf.Clamp(value, 0f,1f); }
    }

    public PlayerSettings() {
        _masterVolume = _sfxVolume = _musicVolume = 1f;
    }
}
