using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;

	private AudioSource audio;

	void Start()
	{
		audio = gameObject.GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.tag == "Player") {
			Instantiate(explosion, coll.transform.position, coll.transform.rotation);
			Destroy(coll.gameObject);
			audio.Play();
			PersistentDataManager.run.GameLost();
			Cursor.visible = true;
		}
	}
}
