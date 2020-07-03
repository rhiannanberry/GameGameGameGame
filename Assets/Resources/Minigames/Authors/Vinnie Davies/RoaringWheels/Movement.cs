using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool isTouching = false;
    AudioClip microphoneInput;
    bool microphoneInitialized;
    public Rigidbody carRigidbody;
    public float speed = 10;
    public float speedFactor;
    public int gravfactor = 3;
    public Collider target;
    public float noise;
    private void Awake()
    {
        if (Microphone.devices.Length > 0)
        {
            microphoneInput = Microphone.Start(null, true, 999, 44100);
            microphoneInitialized = true;
            foreach (var mic in Microphone.devices) {
                print(mic);
            }
        }

        
    }


    // Update is called once per frame
    void Update()
    {
        noise = LevelMax();
        print(noise);
        if (isTouching)
        {
            speed = Mathf.Lerp(speed, 0, Time.deltaTime);
            speed += noise;
            carRigidbody.AddForce(transform.right * speed * speedFactor, ForceMode.Acceleration);
        }
        
    }

    public void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Physics.gravity * gravfactor, ForceMode.Acceleration);
        //RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, 1.5f))
        {
            isTouching = true;
        } else
        {
            isTouching = false;
        }
    }

    float LevelMax()
    {
        int _sampleWindow = 128;
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        microphoneInput.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }
}
