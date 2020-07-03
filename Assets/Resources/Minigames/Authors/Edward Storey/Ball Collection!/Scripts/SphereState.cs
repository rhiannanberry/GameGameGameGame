using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SphereState : MinigameBehaviour
{
    public int count;

    private SphereMovement input;
    private Renderer renderer;
    private AudioSource audio;
    private Rigidbody rb;
    private PipeColor pc;
    private bool playedOnce;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        input = GetComponent<SphereMovement>();
        renderer = GetComponent<Renderer>();
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        pc = GameObject.Find("PipeSystem").GetComponent<PipeColor>();
        playedOnce = true;

        renderer.material.color = pc._scoreColor;
        input.enabled = false;
        rb.Sleep();
    }

    void FixedUpdate()
    {
        if ((int) Countdown.time == 0)
        {
            rb.WakeUp();
            input.enabled = true;
            Countdown.time = -1;
        }
        if (transform.position.y < -1) {
            LoseState();
        }
    }

    void Reset()
    {
        transform.position = new Vector3(1000,(float) 1.5,1000);
        renderer.material.color = pc._scoreColor;
    }

    void OnTriggerEnter(Collider obj)
    {
        GameObject go = obj.gameObject;
        if(go.CompareTag("Trigger"))
        {
            if (go.GetComponentInParent<Renderer>().material.color
                    == renderer.material.color)
                ScoreState();
            else
                LoseState();
        }
    }

    void ScoreState()
    {
        if (count == 0) {
            PersistentDataManager.RUN.GameWon();
            //audio.PlayOneShot(Resources.Load<AudioClip>("Audio/cheer"), 1.0f);
            GameObject.Find("Flavor Text").GetComponent<TextMeshProUGUI>().text = "You Won!";
        }
        else
        {
            pc.Reset();
            Reset();
            SoundManager._PlaySound("score");
            //audio.PlayOneShot(Resources.Load<AudioClip>("Audio/score"), 1.0f);
            count--;
        }
    }

    void LoseState()
    {
        print ("lose");
        if (count > -1)
        {
            PersistentDataManager.RUN.GameLost();

            count = -1;
            //audio.PlayOneShot(Resources.Load<AudioClip>("Audio/boo"), 1.0f);
            GameObject.Find("Flavor Text").GetComponent<TextMeshProUGUI>().text = "You Lost!";
        }
    }

    protected override void OnStateEnter()
    {
    }

    protected override void OnStateExit()
    {
        if (HUDDetails.time <= 0 && count > 0)
        {
            LoseState();
        }
    }
}
