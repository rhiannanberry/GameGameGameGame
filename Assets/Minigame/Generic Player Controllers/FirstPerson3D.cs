using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FirstPerson3D : MinigameBehaviour
{
    [SerializeField] float _turnSpeed;
    private bool _inGame = false;

    // Player Camera

    private Camera _camera;

    // Private Player Movement Variables
    private readonly Vector3 _rotationMin = new Vector3(-60, -360, 0);
    private readonly Vector3 _rotationMax = new Vector3(60, 360, 0);

    private Vector3 _rotationInput;


    // Private Player Interaction Variables
    private Transform _objectLookingAt;

    protected override void Start() {
        base.Start();
        
        _camera = GetComponent<Camera>();
    }

    protected override void OnStateEnter() {
        _inGame = true;
    }

    // Get input in Update
    private void Update() {

        if (!_inGame) return;

        _rotationInput = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f);
    
    }

    // Do movement/physics in FixedUpdate
    private void FixedUpdate() {

        if (!_inGame) return;

        SetCameraRotation();
        SetObjectLookingAt();
        
    }

    protected override void OnStateExit() {
        _inGame = false;
    }

    private void SetCameraRotation() {

        Vector3 newRotation = transform.rotation.eulerAngles + ( _rotationInput * _turnSpeed * Time.deltaTime );
        
        transform.localEulerAngles = newRotation.Clamp( _rotationMin, _rotationMax );

    }

    private void SetObjectLookingAt() {
        RaycastHit hit;
        Ray ray = _camera.ViewportPointToRay( new Vector3( .5f, .5f, 0f ) );

        if (Physics.Raycast(ray, out hit)) {
            _objectLookingAt = hit.transform;
        }
    }



    
}
