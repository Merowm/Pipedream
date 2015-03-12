﻿using UnityEngine;
using System.Collections;

public class HyperspaceGates : MonoBehaviour
{
    private SpaceDriveState spaceDriveState;
    private MovementForward movement;

    void Awake()
    {
        spaceDriveState = transform.parent.parent.GetComponent<SpaceDriveState>();
        movement = transform.parent.parent.GetComponent<MovementForward>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "HyperspaceGateEntrance")
        {
            Debug.Log("Engaging Hyperspace!");
            spaceDriveState.SetDriveStateForward();
            movement.accelerateToHyperspace = true;
        }
        if (other.gameObject.tag == "HyperspaceGateExit")
        {
            Debug.Log("Diengaging Hyperspace!");
            spaceDriveState.SetDriveStateForward();
            spaceDriveState.DisengagingHyperSpace();
            movement.decelerateToSpaceSpeed = true;
        }
    }
}