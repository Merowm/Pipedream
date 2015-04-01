using UnityEngine;
using System.Collections;

public class HyperspaceGates : MonoBehaviour
{
    private SpaceDriveState spaceDriveState;
    private MovementForward movement;
    private EffectControl effects;

    void Awake()
    {
        spaceDriveState = transform.parent.parent.GetComponent<SpaceDriveState>();
        movement = transform.parent.parent.GetComponent<MovementForward>();
        effects = GameObject.FindWithTag("effects").GetComponent<EffectControl>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "HyperspaceGateEntrance")
        {
            Debug.Log("Engaging Hyperspace!");
            //spaceDriveState.SetDriveStateForward();
            //movement.accelerateToHyperspace = true;

            if (spaceDriveState.currentDriveIndex < spaceDriveState.driveStatePositions.Count - 1)
            {
                spaceDriveState.SetDriveStateForward();
                movement.accelerateToHyperspace = true;
            }
            else
            {
                spaceDriveState.SetDriveStateForward();
                spaceDriveState.DisengagingHyperSpace();
                movement.decelerateToSpaceSpeed = true;
            }
        }
        if (other.gameObject.tag == "HyperspaceGateExit")
        {
            Debug.Log("Diengaging Hyperspace!");
            spaceDriveState.SetDriveStateForward();
            spaceDriveState.DisengagingHyperSpace();
            movement.decelerateToSpaceSpeed = true;
            effects.SlowDown();
        }
    }
}
