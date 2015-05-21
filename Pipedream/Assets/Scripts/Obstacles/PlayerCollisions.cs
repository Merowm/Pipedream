using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour
{
    public float maxDodgeSpeed;
    public float dodgeDeceleration;
    public float rightDistance = 0;
    public float leftDistance = 0;

    private Movement2D movement;
    private Health health;
    private Inventory inventory;
    private Transform rightWingtip;
    private Transform leftWingtip;

    void Awake()
    {
        movement = transform.parent.parent.GetComponent<Movement2D>();
        health = transform.parent.GetComponent<Health>();
        inventory = transform.parent.GetComponent<Inventory>();
        rightWingtip = transform.parent.parent.FindChild("RightWingtip").transform;
        leftWingtip = transform.parent.parent.FindChild("LeftWingtip").transform;
    }

	void OnTriggerEnter(Collider other)
	{
        //Obstacle collisions
        if (other.gameObject.tag == "Obstacle")
        {
#if UNITY_ANDROID
            Handheld.Vibrate();
#endif
            Debug.Log("Collision");
            //Activate invulnerability if possible
            inventory.UseItem(1);
            //Reduce points
            other.transform.parent.parent.GetComponentInChildren<ReducePoints>().HitObstacle(false);
            //Take damage
            health.Damage();
        }
        //Collectible collisions
        if (other.gameObject.tag == "Collectible")
        {
            other.GetComponentInParent<CollectPoints>().HitCollectable();
        }
        //Pickup collisions
        if (other.gameObject.tag == "Pickup")
        {
            other.GetComponentInParent<Pickup>().Collect();
        }
	}
}
