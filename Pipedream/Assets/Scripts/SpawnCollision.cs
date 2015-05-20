using UnityEngine;
using System.Collections;

public class SpawnCollision : MonoBehaviour
{
    public bool colliding = false;

    private GameObject player;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

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
        float distanceToPlayer = transform.position.z - player.transform.position.z;

        if (!colliding && distanceToPlayer <= 200.0f)
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
