using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AirPlaneMoviment))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private ROFWeapon weapon;
    [SerializeField] private ROFWeapon[] weapons;
    private AirPlaneMoviment airPlaneMovement;

    private void Awake()
    {
        airPlaneMovement = GetComponent<AirPlaneMoviment>();

        if (weapons[0] != null)
        {
            weapon = weapons[0];
        }
    }

    private void Update()
    {
        var frameInput = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));
        airPlaneMovement.SetSteerInput(frameInput);

        if (weapon != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                weapon.StartShoot();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                weapon.StopShoot();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            weapon = weapon == weapons[0] ? weapons[1] : weapons[0];
        }

    }
}
