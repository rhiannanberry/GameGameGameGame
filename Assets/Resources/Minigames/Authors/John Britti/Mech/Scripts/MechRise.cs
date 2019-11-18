using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechRise : MinigameBehaviour {
    Animator a;
    AudioSource source;
    public GameObject skinnedMesh;
    public GameObject mesh;
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        a = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        Invoke("StartRise", 3);
    }

    protected override void OnStateEnter() {
    }

    protected override void OnStateExit() {

    }

    void StartRise() {
        a.SetTrigger("Rise");
        SoundManager._PlaySound("liftarrive");
        //source.Play();
    }

    // Update is called once per frame
    void Update() {
        if (a.GetCurrentAnimatorStateInfo(0).IsName("Idle") && skinnedMesh.activeSelf) {
            skinnedMesh.SetActive(false);
            mesh.SetActive(true);
        }
    }
}