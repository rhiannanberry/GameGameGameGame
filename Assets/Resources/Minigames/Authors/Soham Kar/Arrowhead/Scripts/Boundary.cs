using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{

	private Vector2 screenBounds;
	public Camera cam;
	
    // Start is called before the first frame update
    void Start()
    {
		cam = Camera.main;
		screenBounds = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
    }

    // Update is called once per frame
    void LateUpdate()
    {
		Vector3 viewPos = transform.position;
		viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x, screenBounds.x * -1);
		viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y, screenBounds.y * -1);
		transform.position = viewPos;
    }
}