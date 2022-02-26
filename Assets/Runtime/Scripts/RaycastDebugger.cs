using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDebugger : MonoBehaviour
{
    // origem = transform.position
    // forward = transform.forward
    [SerializeField] private float rayDistance = 1;
    [SerializeField] private LayerMask collisionMask;

    private Vector3 RayOrigin => transform.position;
    private Vector3 RayDirection => transform.forward;

    private void FixedUpdate()
    {
        Debug.DrawRay(RayOrigin, RayDirection * rayDistance, Color.green);
        if (Physics.Raycast(RayOrigin, RayDirection, out var hit, rayDistance, collisionMask, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(RayOrigin, hit.point, Color.red);
            Debug.Log(hit.collider.name);
            Debug.DrawRay(hit.point, hit.normal, Color.blue);
            Debug.DrawRay(RayOrigin + transform.up * 0.1f, RayDirection * hit.distance, Color.cyan);
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(RayOrigin, RayDirection * rayDistance);
        }
    }
}
