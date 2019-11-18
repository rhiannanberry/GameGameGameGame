using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXExternalBehaviour : MonoBehaviour
{
    public AudioFile SFX;

    // Start is called before the first frame update
    void Start()
    {
        SFX.source = gameObject.GetComponent<AudioSource>();
        if (SFX.source == null) {
            gameObject.AddComponent<AudioSource>();
            SFX.source.clip = SFX.audioClip;
            SFX.source.volume = SFX.volume;
            SFX.source.loop = false;
            SFX.source.playOnAwake = false;
        } else {
            SFX.volume = SFX.source.volume;
        }

        SoundManager._AddExternalSound(SFX);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
