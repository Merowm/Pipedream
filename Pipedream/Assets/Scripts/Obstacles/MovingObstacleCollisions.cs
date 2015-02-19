using UnityEngine;
using System.Collections;

public class MovingObstacleCollisions : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //Obstacle collides with another obstacle -> both explode
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("COLLSIISON");
            Destroy(other.transform.parent.parent.gameObject);
            Destroy(this.gameObject);
        }
        //Collectible collision
        if (other.gameObject.tag == "Collectible")
        {
            Debug.Log("Obstacle hit a collectible!");
        }
    }
}
