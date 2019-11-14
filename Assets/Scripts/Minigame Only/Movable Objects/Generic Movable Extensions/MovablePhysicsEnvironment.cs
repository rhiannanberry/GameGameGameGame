using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePhysicsEnvironment : Movable
{

    private Rigidbody rb;
    
    protected override void Start() {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }

    override protected void ReadInput() {
        rb.constraints = RigidbodyConstraints.None;
    }

    override protected void ClearInput() {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    override protected void Tick() {}
}
