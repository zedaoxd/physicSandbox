using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocity : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var rb = GetComponent<Rigidbody>();
            rb.velocity = velocity;
            enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, velocity);
    }
}
