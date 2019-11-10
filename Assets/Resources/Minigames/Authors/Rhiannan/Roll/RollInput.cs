using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollInput : Movable
{
    public GameObject Director;
    private Vector3 moveDirection;
    private float cameraRotation;
    Rigidbody rb;
    override protected void Start() {
        base.Start();
        rb = GetComponent<Rigidbody>();
        //rb.gravityScale = 1;
    }

    override protected void ReadInput() {
        rb.constraints = RigidbodyConstraints.None;
        moveDirection = Input.GetAxis("Vertical") * Time.deltaTime * Vector3.Cross(Vector3.up, -Director.transform.right);
        moveDirection += Input.GetAxis("Horizontal") * Time.deltaTime * Vector3.Cross(Vector3.up, Director.transform.forward);
        
    }

    override protected void ClearInput() {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        moveDirection = Vector2.zero;
        cameraRotation = 0;
    }

    override protected void Tick() {
        Director.transform.Rotate(0,cameraRotation,0, Space.World);
        rb.AddForce(moveDirection * movableSettings.MoveSpeed);
        Director.transform.position = transform.position;
    }
    
}
