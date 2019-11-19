using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireLead : MonoBehaviour
{
    public Vector3 grabOffset = new Vector3(0, 0, 0.5f);
    public bool Grabbed = false;
    public float grabSmooth = 1;
    public float holeSmooth = 10;
    public Transform Model;
    public InputHole hole;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Model.localPosition = Vector3.Lerp(Model.localPosition, Grabbed ? grabOffset : Vector3.zero, Time.deltaTime * grabSmooth);
        if (hole) {
            transform.position = Vector3.Lerp(transform.position, hole.transform.position, Time.deltaTime * holeSmooth);
        }
    }
}
