using UnityEngine;
using System.Collections;

public class Collisions : MonoBehaviour
{
    public MovementForward movement;

    void Awake()
    {
        movement = transform.parent.GetComponent<MovementForward>();
    }

	void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Obstacle")
        {
            //Collision in hyperspace
            if (movement.inHyperSpace)
            {
                Debug.Log("Hyperspace collison");
                //TODO:Make player "avoid" collision just before hitting the obstacle
            }
            //Collision out of hyperspace
            else
            {
                Debug.Log("Space collision");
                Destroy(other.gameObject);
                //TODO:Add explosion
            }
        }
	}
}
