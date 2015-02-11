using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public float distanceFromTarget = 10f;
	public float dampTime = 0.15f;
	public Transform target;

    private Transform mainCamera;
    //private MovementForward targetMovement;
	private Vector3 velocity = Vector3.zero;

	void Awake ()
	{
        if (target)
        {
            mainCamera = transform.GetChild(0).transform;
            //targetMovement = GameObject.Find("Player").GetComponent<MovementForward>();
        }
	}

	void FixedUpdate ()
	{
		if (target)
		{
            transform.position = target.parent.position;

            if (MovementForward.inHyperSpace)
            {
                mainCamera.position = new Vector3(target.parent.position.x,
                                                  target.parent.position.y,
                                                  target.position.z - distanceFromTarget);
                transform.Rotate(Vector3.forward *
                                  Time.deltaTime * 
                                  (target.parent.GetComponent<Movement2D>().currentRotationSpeed * 10));
            }
            else
            {
                transform.rotation = new Quaternion(0,0,0,0);
                Vector3 delta = target.position - mainCamera.camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
                Vector3 destination = mainCamera.position + delta;
                mainCamera.position = Vector3.SmoothDamp(mainCamera.position,
                                                         new Vector3(target.position.x,target.position.y + 2,destination.z - distanceFromTarget),
                                                         ref velocity,
                                                         dampTime);
                    
                //mainCamera.position = new Vector3(target.position.x,
                //                                  target.position.y + 2,
                //                                  target.position.z - distanceFromTarget);
            }
		}
	}
}
