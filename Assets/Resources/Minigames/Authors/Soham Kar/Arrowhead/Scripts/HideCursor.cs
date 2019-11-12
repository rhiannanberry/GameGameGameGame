using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCursor : MinigameBehaviour
{
	private AudioSource audio;

	private bool inMinigame = false;

	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();
		audio = gameObject.GetComponent<AudioSource>();
	}

	protected override void OnStateEnter() {
		Cursor.visible = false;
		inMinigame = true;
		audio.Play();
    }

    protected override void OnStateExit() {
        Cursor.visible = true;
		inMinigame = false;
		PauseAudio();
    }

	// Update is called once per frame
	void FixedUpdate()
	{
		Event currentEvent = Event.current;
		if (inMinigame) {
			Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3
				(Input.mousePosition.x, Input.mousePosition.y, 10));
			transform.position = pos;
		}
	}

	void PauseAudio()
	{
		audio.Pause();
	}
}