using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class Ball : Movable
    {

        
        [SerializeField] private float _maxAngularVelocity = 25; // The maximum velocity the ball can rotate at.

        private Rigidbody _rb;
        private Vector3 _moveDirection;


        override protected void Start() {
            base.Start();
            _rb = GetComponent<Rigidbody>();
            // Set the maximum angular velocity.
            GetComponent<Rigidbody>().maxAngularVelocity = _maxAngularVelocity;
        }

        override protected void ReadInput() {
            _rb.constraints = RigidbodyConstraints.None;
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (Camera.main != null)
            {
                // calculate camera relative direction to move:
                Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                _moveDirection = (v*camForward + h*Camera.main.transform.right).normalized;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                _moveDirection = (v*Vector3.forward + h*Vector3.right).normalized;
            }
        }

        override protected void ClearInput() {
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            _moveDirection = Vector3.zero;
        }

        override protected void Tick() {
            if (movableSettings.UseTorque)
            {
                // ... add torque around the axis defined by the move direction.
                _rb.AddTorque(new Vector3(_moveDirection.z, 0, -_moveDirection.x)*movableSettings.MoveSpeed);
            }
            else
            {
                // Otherwise add force in the move direction.
                _rb.AddForce(_moveDirection*movableSettings.MoveSpeed);
            }
        }
    }
}
