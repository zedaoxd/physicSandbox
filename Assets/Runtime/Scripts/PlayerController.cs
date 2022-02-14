using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AirPlaneMoviment))]
public class PlayerController : MonoBehaviour
{
    private AirPlaneMoviment airPlane;

    private void Awake()
    {
        airPlane = GetComponent<AirPlaneMoviment>();
    }

    private void Update()
    {
        var frameInput = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));
        airPlane.SetSteerInput(frameInput);
    }
}
