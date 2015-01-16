using UnityEngine;
using System.Collections;

public class LaneMovement : MonoBehaviour
{
	public float speed;
	public float maxSpeed;
	public float minSpeed;
	public float acceleration;
	public float deceleration;
	public Vector3 direction;
	public float rotationSpeed = 100f;

	private float temp;
	private Transform tunnel;
	private Transform mainCamera;
	
	void Start ()
	{
		tunnel = GameObject.FindGameObjectWithTag("Tunnel").transform;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
	
	void FixedUpdate ()
	{
		if (Input.GetKeyDown(KeyCode.D))
		{
			temp = transform.rotation.z + rotationSpeed;
			Debug.Log (temp);
		}
		if (transform.rotation.z < temp)
		{
			transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
		}
		Debug.Log (temp);

		if (Input.GetKeyDown(KeyCode.A))
		{
			//transform.Rotate(-Vector3.forward * Time.deltaTime * rotationSpeed);
			transform.Rotate(-Vector3.forward * rotationSpeed);
		}
		if (Input.GetKey(KeyCode.W) && speed < maxSpeed)
		{
			speed += acceleration * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S) && speed > minSpeed)
		{
			speed -= deceleration * Time.deltaTime;
		}
		
		Vector3 position = transform.position;
		position += direction * speed * Time.deltaTime;
		transform.position = position;
	}
}
