using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetObject;

    void Start()
    {

    }

    void Update()
    {
        transform.LookAt(targetObject.transform.position);
    }
}
