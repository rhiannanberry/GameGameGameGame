using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public string destructionClipName;

	public void TriggerDestruction() {
		SoundManager._PlaySound(destructionClipName);
		Instantiate(explosion, transform.position, transform.rotation);
		Cursor.visible = true;
		Destroy(gameObject);
	}
}
