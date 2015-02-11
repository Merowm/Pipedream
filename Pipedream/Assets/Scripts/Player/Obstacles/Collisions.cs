using UnityEngine;
using System.Collections;

public class Collisions : MonoBehaviour
{
    void Awake()
    {

    }

	void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Obstacle")
        {
            //Collision in hyperspace
            if (MovementForward.inHyperSpace)
            {
                Debug.Log("Hyperspace collison");
                //TODO:Make player "avoid" collision just before hitting the obstacle
                Debug.Break();
            }
            //Collision out of hyperspace
            else
            {
                Debug.Log("Space collision");
                Destroy(other.gameObject);
                //TODO:Add explosion
                Debug.Break();
            }
        }
	}
}
