using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grabber : MonoBehaviour
{
    WireLead grabbed;
    public float grabLerp = 10;
    public AudioClip plugIn;
    public AudioClip unPLug;
    private AudioSource[] audioSources = new AudioSource[2];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < audioSources.Length; i++) {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
        }
        audioSources[0].clip = plugIn;
        audioSources[1].clip = unPLug;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Input.GetButtonDown("Fire1") && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)){
            var lead = hit.transform.GetComponent<WireLead>();
            if (lead) {
                if (grabbed) {
                    grabbed.Grabbed = false;
                    grabbed.GetComponent<Collider>().enabled = true;
                    grabbed.hole = lead.hole;
                    grabbed = null;
                    audioSources[0].pitch = Random.Range(0.7f, 1.3f);
                    audioSources[0].Play();
                }
                audioSources[1].pitch = Random.Range(0.7f, 1.3f);
                audioSources[1].Play();
                grabbed = lead;
                grabbed.Grabbed = true;
                grabbed.GetComponent<Collider>().enabled = false;
                grabbed.hole = null;
            }
            var hole = hit.transform.GetComponent<InputHole>();
            if (hole && grabbed) {
                audioSources[0].pitch = Random.Range(0.7f, 1.3f);
                audioSources[0].Play();
                grabbed.Grabbed = false;
                grabbed.GetComponent<Collider>().enabled = true;
                grabbed.hole = hole;
                grabbed = null;
                if (SwitchBoardController.instance.wires.All(w => w.CorrectHoles())) {
                    PersistentDataManager.RUN.GameWon();
                }
            }
        }
        if (grabbed) {
            var mousePos = Input.mousePosition;
            mousePos.z = Camera.main.transform.position.z;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            grabbed.transform.position = Vector3.Lerp(
                grabbed.transform.position,
                new Vector3(mousePos.x, mousePos.y, grabbed.transform.position.z),
                Time.deltaTime * grabLerp
            );
        }
    }
}
