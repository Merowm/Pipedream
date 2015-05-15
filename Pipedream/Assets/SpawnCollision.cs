using UnityEngine;
using System.Collections;

public class SpawnCollision : MonoBehaviour
{
    public bool colliding = false;

    void FixedUpdate ()
    {
        if (colliding)
        {
            Vector3 rotation = new Vector3(0,0,Random.Range(0.0f,360.0f));
            transform.parent.parent.transform.Rotate(rotation);
            colliding = false;
        }
    }

    void Update ()
    {
        if (!colliding)
        {
            transform.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag != "Whoosh" && other.tag != "Pickup")
        {
            colliding = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Whoosh" && other.tag != "Pickup")
        {
            colliding = true;
        }
    }
}
