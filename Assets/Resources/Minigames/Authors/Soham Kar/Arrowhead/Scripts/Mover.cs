using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	public float speed;
	[HideInInspector] public GameController gc;
	private Rigidbody2D rb;

	private float _time = 0f;

	private bool inMinigame = false;

	private void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
	}

	private void OnStateEnter() {
		rb.velocity = transform.up * speed;
        rb.constraints = RigidbodyConstraints2D.None;
    }

    	private void OnStateExit() {
		rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

	private void Update() {
		if (gc.inMinigame && !inMinigame) {
			inMinigame = true;
			OnStateEnter();

		} else if (!gc.inMinigame && inMinigame) {
			inMinigame = false;
			OnStateExit();
		}
		if (inMinigame) {
			_time += Time.deltaTime;
			if (_time >= 5f) Destroy(gameObject);
		}
	}
}
