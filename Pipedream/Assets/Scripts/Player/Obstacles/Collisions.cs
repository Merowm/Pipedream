using UnityEngine;
using System.Collections;

public class Collisions : MonoBehaviour
{
    public float maxDodgeSpeed;
    public float dodgeAcceleration;
    public float dodgeDeceleration;

    private Movement2D movement;

    void Awake()
    {
        movement = transform.parent.GetComponent<Movement2D>();
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
                movement.ForcedDodge();
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
