using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public int movementSpeed;
    public int jumpSpeed;

    private Rigidbody rb;
    private bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grounded = true;
    }

    void FixedUpdate()
    {
            if (Input.GetAxis("Jump") != 0 && grounded)
            {

                rb.AddForce(Vector3.up * jumpSpeed * Input.GetAxis("Jump"));
                grounded = false;
            }
            rb.AddForce(Vector3.forward * movementSpeed
                    * Input.GetAxis("Vertical") );
            rb.AddForce(Vector3.left * movementSpeed
                    * Input.GetAxis("Horizontal"));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}
