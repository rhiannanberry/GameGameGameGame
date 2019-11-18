using UnityEngine;
using System.Collections;

public class TimedObjectDestructor : MinigameBehaviour {

	public float timeOut = 1.0f;
	public bool detachChildren = false;

	private bool _canCount = false;
	private float _time = 0f;


	protected override void OnStateEnter() {
        _canCount = true;
    }

    protected override void OnStateExit() {
        _canCount = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (_canCount == false) return;
		
		_time += Time.deltaTime;
		if (_time >= timeOut) DestroyNow();
	}
	
	void DestroyNow ()
	{
		if (detachChildren) { // detach the children before destroying if specified
			transform.DetachChildren ();
		}
		GameObject.Destroy(gameObject);
	}
}
