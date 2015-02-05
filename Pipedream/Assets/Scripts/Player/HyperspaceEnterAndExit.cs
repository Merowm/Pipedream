using UnityEngine;
using System.Collections;

public class HyperspaceEnterAndExit : MonoBehaviour
{
    private MovementForward movement;

    void Awake()
    {
        movement = transform.parent.GetComponent<MovementForward>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "HyperspaceGateEntrance")
        {
            Debug.Log("Engaging Hyperspace!");
            movement.accelerateToHyperspace = true;
        }
        if (other.gameObject.tag == "HyperspaceGateExit")
        {
            Debug.Log("Diengaging Hyperspace!");
            movement.DisengagingHyperSpace();
            movement.decelerateToSpaceSpeed = true;
        }
    }
}
