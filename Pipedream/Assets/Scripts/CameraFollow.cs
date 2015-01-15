using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public float distanceFromPlayer = 10f;
	public float dampTime = 0.15f;
	public Transform target;

	private Vector3 velocity = Vector3.zero;
	
	void FixedUpdate ()
	{
		if (target)
		{
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0,0,destination.z - distanceFromPlayer), ref velocity, dampTime);
		}
	}
}
