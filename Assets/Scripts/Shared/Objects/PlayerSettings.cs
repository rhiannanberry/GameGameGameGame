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

    public PlayerSettings Copy() {
        return (PlayerSettings)this.MemberwiseClone();
    }

    public override bool Equals(object obj) {
        PlayerSettings other = (PlayerSettings)obj;
        return (MasterVolume == other.MasterVolume) && (MusicVolume == other.MusicVolume) && (SFXVolume == other.SFXVolume);
    }

    public override string ToString() {
        string str = "Master Volume: " + _masterVolume + "\n" +
                    "Music Volume: " + _musicVolume + "\n" +
                    "SFX Volume: " + _sfxVolume + "\n";
        return str;
    }
}
