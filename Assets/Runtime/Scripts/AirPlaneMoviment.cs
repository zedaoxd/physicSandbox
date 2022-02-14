using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[DisallowMultipleComponent]
public class AirPlaneMoviment : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector2 _frameInput;
    private Vector2 _steerInput;
    [SerializeField] private float steerSmoothSpeed = 1.5f;
    
    [Header("Physics")]
    [SerializeField] private float pitchTorque = 30;
    [SerializeField] private float yawTorque = 15;
    [SerializeField] private float thrust = 60;

    [Header("Rigidbody")] 
    [SerializeField] private float mass = 10;
    [SerializeField] private float drag = 0.5f;
    [SerializeField] private float angularDrag = 10;
    

    private void Awake()
    {
        InitializeRigidbody();
    }

    private void InitializeRigidbody()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.mass = mass;
        _rb.drag = drag;
        _rb.angularDrag = angularDrag;
    }

    private void FixedUpdate()
    {
        UpdateMovement();
        UpdateSteering();
    }

    private void UpdateSteering()
    {
        _steerInput = Vector2.Lerp(_steerInput, _frameInput, Time.fixedTime * steerSmoothSpeed);
        var torque = new Vector3(
            _steerInput.x * pitchTorque,
            _steerInput.y * yawTorque,
            0
        );

        _rb.AddRelativeTorque(torque);
    }

    private void UpdateMovement()
    {
        var moveForce = transform.forward * thrust;
        _rb.AddForce(moveForce);
    }

    public void SetSteerInput(Vector2 newFrameInput)
    {
        _frameInput = newFrameInput;
    }
}
