using UnityEngine;
using System.Collections;

public class SpawnCollision : MonoBehaviour
{
    public bool colliding = false;

    private GameObject player;
    public int count = 0;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    void FixedUpdate ()
    {
        if (colliding && count < 5)
        {
            Vector3 rotation = new Vector3(0,0,Random.Range(0.0f,360.0f));
            transform.parent.parent.transform.Rotate(rotation);
            count++;
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
            if (count >= 5)
            {
                //other.transform.parent.gameObject.SetActive(false);
                transform.parent.gameObject.SetActive(false);
            }
            colliding = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Whoosh" && other.tag != "Pickup")
        {
            if (count >= 5)
            {
                //other.transform.parent.gameObject.SetActive(false);
                transform.parent.gameObject.SetActive(false);
            }
            colliding = true;
        }
    }
}
