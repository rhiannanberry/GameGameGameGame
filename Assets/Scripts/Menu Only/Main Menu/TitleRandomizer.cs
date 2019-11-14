using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleRandomizer : MonoBehaviour {
    [SerializeField] private Vector2 screenPadding = new Vector2(0.1f, 0.05f);
    [SerializeField] private float randomInterval;

    private void OnValidate() {
        randomInterval = randomInterval <= 0 ? 0.1f : randomInterval;
    }

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("Randomize", randomInterval, randomInterval);
    }

    void Randomize() {
        Vector2 randPos = new Vector2(Random.Range(Screen.width * screenPadding.x, Screen.width * (1 - screenPadding.x)), Random.Range(Screen.height * screenPadding.y, Screen.height * (1 - screenPadding.y)));
        transform.position = randPos;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.localScale = new Vector3(Random.Range(0.5f, 2.5f), Random.Range(0.5f, 2.5f), 1);
    }
}