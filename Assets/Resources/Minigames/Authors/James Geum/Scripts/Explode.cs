using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;

    public void ExplodeSelf() {
        if (_explosionPrefab!=null) {
            Instantiate (_explosionPrefab, transform.position, Quaternion.identity);
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
