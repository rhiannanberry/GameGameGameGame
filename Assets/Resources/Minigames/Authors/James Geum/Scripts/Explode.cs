using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab = null;
    [SerializeField] private string _explosionSFX = "";

    public void ExplodeSelf() {
        if (_explosionPrefab!=null) {
            Instantiate (_explosionPrefab, transform.position, Quaternion.identity);
            SoundManager._PlaySound(_explosionSFX);
        }
        Destroy(gameObject);
    }

    public void ExplodeOther() {
        Collisions col = GetComponent<Collisions>();
        if (col == null) return;
        Explode exp = col.lastCollided.GetComponent<Explode>();
        if (exp == null) return;
        exp.ExplodeSelf();
    }
}
