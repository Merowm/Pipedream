using UnityEngine;
using System.Collections;

public class Wireframe : MonoBehaviour
{
    public float displayDistance = 10.0f;
    public float distance = 0.0f;

    private Transform player;
    //private RaycastHit hit;
    
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void FixedUpdate ()
    {
        if (MovementForward.inHyperSpace)
        {
            distance = Vector3.Distance(player.transform.position, transform.parent.parent.position);

            if (distance <= 300.0f)
            {
                Debug.Log("Wireframe activated!");

                transform.position = new Vector3(transform.position.x,
                                                 transform.position.y,
                                                 player.transform.position.z + displayDistance);
                //Debug.Break();
            }

            if (transform.parent.transform.position.z <= player.transform.position.z + displayDistance)
            {
                transform.position = transform.parent.position;
                //Debug.Break();
            }

            //if (Physics.Raycast(transform.position,new Vector3(0,0,1),out hit,))
        }
    }
}
