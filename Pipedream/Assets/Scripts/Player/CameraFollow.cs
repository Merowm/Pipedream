using UnityEngine;
using System.Collections;
//TODO:G-forces affect distanceFromTarget, faster speed: camera is farther back, slower speed: camera is closer to the player
public class CameraFollow : MonoBehaviour
{
    public float distanceFromTarget = 10f;
    public float temp; //TODO: Name better, this is used to give an impression of speed
	public float dampTime = 0.15f;
	public Transform target;

	private Vector3 velocity = Vector3.zero;
    private MovementForward targetMovement;
    private float currentSpeed;
    private float currentSpeedLastFrame;

	void Awake ()
	{
        targetMovement = target.GetComponent<MovementForward>();
	}

	void FixedUpdate ()
	{
		if (target)
		{
            //if (target.GetComponent<MovementForward>().currentSpeed > 7.5f)
            //{
            //    distanceFromTarget = target.GetComponent<MovementForward>().currentSpeed;
            //}
            //else distanceFromTarget = 7.5f;

            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position,
                                                    new Vector3(transform.position.x,
                                                    0,
                                                    destination.z - distanceFromTarget),
                                                    ref velocity,
                                                    dampTime);

            //  currentSpeed = targetMovement.currentSpeed;
            // temp = 1.0f / 2.0f;
            //if(targetMovement.currentSpeed != currentSpeedLastFrame)
            // {
            
            //     temp = targetMovement.currentSpeed / targetMovement.maxSpaceSpeed;
            //     distanceFromTarget *= targetMovement.currentSpeed;
            // Debug.Log("asd");
            //}
                
            // currentSpeedLastFrame = targetMovement.currentSpeed;
                
            transform.position = new Vector3(transform.position.x,
                                             transform.position.y,
                                             target.position.z - distanceFromTarget);
            
            transform.Rotate(Vector3.forward *
                             Time.deltaTime * 
                             target.GetComponent<Movement2D>().currentRotationSpeed * 10);
		}
	}
}
