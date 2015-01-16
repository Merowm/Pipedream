using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	public float speed;
	public float maxSpeed;
	public float minSpeed;
	public float acceleration;
	public float deceleration;
	public Vector3 direction;
	public float rotationSpeed = 100f;

	private Transform tunnel;
	private Transform mainCamera;

	void Start ()
	{
		tunnel = GameObject.FindGameObjectWithTag("Tunnel").transform;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}

	void FixedUpdate ()
	{
		//direction = new Vector3 (0,0,direction.z);

		if (Input.GetKey(KeyCode.D))
		{
			//direction = new Vector3(direction.x + 10,0,direction.z);
			//rbody.AddForce(new Vector3(50,0,0));
			//tunnel.transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
			transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
			//mainCamera.transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
		}
		if (Input.GetKey(KeyCode.A))
		{
			//direction = new Vector3(direction.x - 10,0,direction.z);
			//rbody.AddForce(new Vector3(-50,0,0));
			//tunnel.transform.Rotate(-Vector3.forward * Time.deltaTime * rotationSpeed);
			transform.Rotate(-Vector3.forward * Time.deltaTime * rotationSpeed);
			//mainCamera.transform.Rotate(-Vector3.forward * Time.deltaTime * rotationSpeed);
		}
		if (Input.GetKey(KeyCode.W) && speed < maxSpeed)
		{
			speed += acceleration * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S) && speed > minSpeed)
		{
			speed -= deceleration * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.Q) && speed > minSpeed)
		{
			Application.Quit();
		}

		Vector3 position = transform.position;
		position += direction * speed * Time.deltaTime;
		transform.position = position;
	}
}
