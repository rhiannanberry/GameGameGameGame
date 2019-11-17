using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyCamera : MonoBehaviour {
    Vector3 startPos;
    [SerializeField] private Transform cam;
    [SerializeField] private Vector3 endPos;
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float rotSpeed = 0.1f;
    [SerializeField] private float smoothSpeed = 5;
    [SerializeField] private float angleLimit = 30;
    private float vertical = 0;
    private float horizontal = 0.5f;
    // Start is called before the first frame update
    void Start() {
        startPos = cam.transform.localPosition;
    }

    // Update is called once per frame
    void Update() {
        vertical = Mathf.Clamp01(vertical + speed * Input.GetAxis("Vertical"));
        horizontal = Mathf.Clamp01(horizontal + rotSpeed * Input.GetAxis("Horizontal"));
        cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, Vector3.Lerp(startPos, endPos, vertical), Time.deltaTime * smoothSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, Mathf.Lerp(angleLimit, -angleLimit, horizontal), 0), Time.deltaTime * smoothSpeed);
    }
}