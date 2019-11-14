using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MinigameBehaviour {

	public GameObject spawnPrefab;

	public float minSecondsBetweenSpawning = 3.0f;
	public float maxSecondsBetweenSpawning = 6.0f;
	
	public Transform chaseTarget;
	
	private float savedTime;
	private float secondsBetweenSpawning;

	private bool canSpawn = false;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
	}

	protected override void OnStateEnter() {
        canSpawn = true;
		savedTime = Time.time;
    }

    protected override void OnStateExit() {
        canSpawn = false;
    }
	
	// Update is called once per frame
	void Update () {

		if (canSpawn && (Time.time - savedTime >= secondsBetweenSpawning)) // is it time to spawn again?
		{
			MakeThingToSpawn();
			savedTime = Time.time; // store for next spawn
			secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
		}	
	}

	void MakeThingToSpawn()
	{
		// create a new gameObject
		GameObject clone = Instantiate(spawnPrefab, transform.position, transform.rotation) as GameObject;

		// set chaseTarget if specified
		if ((chaseTarget != null) && (clone.gameObject.GetComponent<Chaser> () != null))
		{
			clone.gameObject.GetComponent<Chaser>().SetTarget(chaseTarget);
		}
	}
}
