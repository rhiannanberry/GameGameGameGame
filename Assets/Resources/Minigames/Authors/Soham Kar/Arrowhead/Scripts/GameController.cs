using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MinigameBehaviour
{
	public GameObject hazard;
	public GameObject hazard1;

	private GameObject clone;
	private GameObject clone1;

	public Vector3 spawnValues;
	public Vector3 treeSpawnValues;

	public float startWait;
	public float spawnWait;
	public float treeStartWait;
	public float treeSpawnWait;

	//private float timer = 0;
	[HideInInspector] public bool inMinigame = false;
	private IEnumerator waves, wavesTrees;

	protected override void Start()
	{
		base.Start();
		waves = SpawnWaves();
		wavesTrees = SpawnWavesTrees();
	}

	protected override void OnStateEnter() {
		inMinigame = true;
		StartCoroutine(waves);
		StartCoroutine(wavesTrees);
    }

    protected override void OnStateExit() {
        inMinigame = false;
		StopCoroutine(waves);
		StopCoroutine(wavesTrees);
    }

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait);
		while (true)
		{
			if (inMinigame) {
				Vector3 spawnPosition = new Vector3((Random.Range(-spawnValues.x, spawnValues.x)), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				clone = Instantiate(hazard, spawnPosition, spawnRotation);
				clone.GetComponent<Mover>().gc = this;
			}
			yield return new WaitForSeconds(spawnWait);
		}
	}

	IEnumerator SpawnWavesTrees()
	{
		yield return new WaitForSeconds(treeStartWait);
		while (true)
		{
			if(inMinigame) {
				Vector3 spawnPosition = new Vector3((Random.Range(-treeSpawnValues.x, treeSpawnValues.x)), treeSpawnValues.y, treeSpawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				clone1 = Instantiate(hazard1, spawnPosition, spawnRotation);
				clone1.GetComponent<Mover>().gc = this;
			}
			yield return new WaitForSeconds(treeSpawnWait);
		}
	}

}
