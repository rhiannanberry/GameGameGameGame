using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MinigameBehaviour
{
	public float speed;
	private Rigidbody2D rb;

	private float _time = 0f;

	private bool inMinigame = false;

	protected override void Start ()
	{
		base.Start();
		rb = gameObject.GetComponent<Rigidbody2D>();
		OnStateEnter();
		
	}

	protected override void OnStateEnter() {
		inMinigame = true;
		rb.velocity = transform.up * speed;
        rb.constraints = RigidbodyConstraints2D.None;
    }

    protected override void OnStateExit() {
		inMinigame = false;
		rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

	private void Update() {
		if (!inMinigame) return;
		_time += Time.deltaTime;
		if (_time >= 5f) Destroy(gameObject);
	}
}
