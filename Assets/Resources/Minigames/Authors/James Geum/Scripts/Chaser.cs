using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]

public class Chaser : MinigameBehaviour {
	
	public float speed = 20.0f;
	public float minDist = 1f;
	public Transform target;

	private bool canMove = true;

	protected override void OnStateEnter() {
		if (target == null) {

			if (GameObject.FindWithTag ("Player")!=null)
			{
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
        canMove = true;
    }

    protected override void OnStateExit() {
        canMove = false;
    }
	
	// Update is called once per frame
	void Update () 
	{
		if (canMove == false) return;

		if (target == null)
			return;

		// face the target
		transform.LookAt(target);

		//get the distance between the chaser and the target
		float distance = Vector3.Distance(transform.position,target.position);

		//so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
		if(distance > minDist)	
			transform.position += transform.forward * speed * Time.deltaTime;	
	}

	// Set the target of the chaser
	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}

}
