using UnityEngine;
using System.Collections;

public class Wireframe : MonoBehaviour
{
    public float displayDistance = 10.0f;

    private float distance = 0.0f;
    private Transform player;
    
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void FixedUpdate ()
    {
        if (MovementForward.inHyperSpace)
        {
            distance = Vector3.Distance(player.transform.position, transform.parent.parent.position);

            if (distance <= displayDistance)
            {
                transform.GetComponent<MeshRenderer>().enabled = true;
                transform.GetComponent<Animator>().enabled = true;
                transform.position = new Vector3(transform.position.x,
                                                 transform.position.y,
                                                 player.transform.position.z + displayDistance);
            }

            if (transform.parent.transform.position.z <= player.transform.position.z + displayDistance)
            {
                transform.position = transform.parent.position;
            }
        }
    }
}
