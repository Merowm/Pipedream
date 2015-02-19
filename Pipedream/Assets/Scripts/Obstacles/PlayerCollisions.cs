using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour
{
    public float maxDodgeSpeed;
    public float dodgeDeceleration;
    public float rightDistance = 0;
    public float leftDistance = 0;

    private Movement2D movement;
    private Transform rightWingtip;
    private Transform leftWingtip;

    void Awake()
    {
        movement = transform.parent.GetComponent<Movement2D>();
        rightWingtip = transform.parent.FindChild("RightWingtip").transform;
        leftWingtip = transform.parent.FindChild("LeftWingtip").transform;
    }

	void OnTriggerEnter(Collider other)
	{
        //Obstacle collisions
        if (other.gameObject.tag == "Obstacle")
        {
            //Collision in hyperspace
            if (MovementForward.inHyperSpace)
            {
                Debug.Log("Hyperspace collison");
                rightDistance = Vector3.Distance(rightWingtip.position, other.transform.position);
                leftDistance = Vector3.Distance(leftWingtip.position, other.transform.position);
                movement.ForcedDodge();
                other.transform.parent.parent.GetComponent<ReducePoints>().HitObstacle(false);
            }
            //Collision out of hyperspace
            else
            {
                Debug.Log("Space collision");
                other.transform.parent.parent.GetComponent<ReducePoints>().HitObstacle(true);
                //TODO:Add explosion
            }
        }
        //Collectible collisions
        if (other.gameObject.tag == "Collectible")
        {
            other.GetComponentInParent<CollectPoints>().HitCollectable();
        }
	}
}
