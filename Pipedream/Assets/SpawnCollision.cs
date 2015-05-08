using UnityEngine;
using System.Collections;

public class SpawnCollision : MonoBehaviour
{
    public bool colliding = false;

    void OnTriggerStay(Collider other)
    {
        if (other.tag != "Whoosh")
        {
            colliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Whoosh")
        {
            colliding = false;
        }
    }
}
