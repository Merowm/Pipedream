using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public float speed;
	public Vector3 direction;

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	
	void FixedUpdate ()
	{
		Vector3 position = transform.position;
		position += direction * speed * Time.deltaTime;
		transform.position = position;

		if (target)
		{
			//Vector3 point = camera.WorldToViewportPoint(target.position);
			//Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
			//Vector3 destination = transform.position + delta;
			//transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}
}
