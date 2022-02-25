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
    [SerializeField] private float xRotClamp = 70;
    
    [Header("Rigidbody")] 
    [SerializeField] private float mass = 10;
    [SerializeField] private float drag = 0.5f;
    [SerializeField] private float angularDrag = 10;

    [Header("Visuals")] 
    [SerializeField] private Transform graphics;
    [SerializeField] private float maxVisualRollAngle = 60;
    [SerializeField] private float visualRollAcc = 2;
    [SerializeField] private GameObject elice;

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

    private static float To180Angle(float angle)
    {
        angle %= 360;
        if (angle > 180) return angle - 360; // greater than 180
        if (angle < -180) return angle + 360; // less than -180 
        return angle; // between -180 and 180 
    }

    private void UpdateSteering()
    {
        // add Torque
        _steerInput = Vector2.Lerp(_steerInput, _frameInput, Time.fixedTime * steerSmoothSpeed);
        var torque = new Vector3(
            _steerInput.x * pitchTorque,
            _steerInput.y * yawTorque,
            0
        );
        
        if (ShouldBlockPitch())
        {
            _steerInput.x = 0;
            torque.x = 0;
        }

        _rb.AddRelativeTorque(torque);

        // Force no Z rotation
        var correctedRot = _rb.rotation.eulerAngles;
        correctedRot.z = 0;
        _rb.rotation = Quaternion.Euler(correctedRot);
        
        // Graphics
        var graphicsLocalRotation = graphics.localEulerAngles;
        var targetZ = -maxVisualRollAngle * _steerInput.y;
        graphicsLocalRotation.z = Mathf.MoveTowardsAngle(
            graphicsLocalRotation.z, 
            targetZ, 
            Time.fixedDeltaTime * visualRollAcc
        );
        graphics.localEulerAngles = graphicsLocalRotation;
    }

    private bool ShouldBlockPitch()
    {
        var xRot = To180Angle(_rb.rotation.eulerAngles.x);
        //Se ja passamos do limite de inclinação e ainda esta tentando mover naquela direção, bloqueia o movimento
        return (_frameInput.x > 0 && xRot > xRotClamp) ||
               (_frameInput.x < 0 && xRot < -xRotClamp);
    }

    private void UpdateMovement()
    {
        var moveForce = transform.forward * thrust;
        _rb.AddForce(moveForce);
        elice.transform.Rotate(new Vector3(-200, 0, 0) * thrust);
    }

    public void SetSteerInput(Vector2 newFrameInput)
    {
        _frameInput = newFrameInput;
    }
}
