using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AirPlaneMoviment), typeof(BombLauncher))]
public class PlayerController : MonoBehaviour
{
    private AirPlaneMoviment airPlane;
    private BombLauncher bombLauncher;

    private void Awake()
    {
        airPlane = GetComponent<AirPlaneMoviment>();
        bombLauncher = GetComponent<BombLauncher>();
    }

    private void Update()
    {
        var frameInput = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));
        airPlane.SetSteerInput(frameInput);

        if (Input.GetKey(KeyCode.Space))
        {
            bombLauncher.TryShoot();
        }
    }
}
