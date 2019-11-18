using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public Rigidbody rb;

	private AudioSource audio;

	private float timer = 0;

	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody>();
		audio = gameObject.GetComponent<AudioSource>();
		audio.PlayDelayed(3.3f);
	}

	void Update()
	{
		timer += Time.deltaTime;
		if (timer > 4) {
			rb.useGravity = true;
		}
		if (timer > 54.5) {
			audio.Pause();
		}
	}

	public void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Exit1"))
		{
			other.gameObject.SetActive(false);
			audio.Pause();
			PersistentDataManager.RUN.GameWon();
		}
	}
}
