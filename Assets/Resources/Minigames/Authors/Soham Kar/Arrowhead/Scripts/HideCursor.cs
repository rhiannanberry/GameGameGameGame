using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCursor : MinigameBehaviour
{
	private bool inMinigame = false;

	protected override void OnStateEnter() {
		Cursor.visible = false;
		inMinigame = true;
    }

    protected override void OnStateExit() {
        Cursor.visible = true;
		inMinigame = false;
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
}