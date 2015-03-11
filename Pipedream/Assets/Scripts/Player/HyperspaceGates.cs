using UnityEngine;
using System.Collections;

public class HyperspaceGates : MonoBehaviour
{
    private SpaceDriveState spaceDriveState;

    void Awake()
    {
        spaceDriveState = transform.parent.parent.GetComponent<SpaceDriveState>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "HyperspaceGateEntrance")
        {
            Debug.Log("Engaging Hyperspace!");
            spaceDriveState.SetDriveStateForward();
            MovementForward.accelerateToHyperspace = true;
        }
        if (other.gameObject.tag == "HyperspaceGateExit")
        {
            Debug.Log("Diengaging Hyperspace!");
            spaceDriveState.SetDriveStateForward();
            spaceDriveState.DisengagingHyperSpace();
            MovementForward.decelerateToSpaceSpeed = true;
        }
    }
}
