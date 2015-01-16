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
	public float rotationSpeed2 = 100f;

	private Transform tunnel;
	private Transform mainCamera;

	private float rotation = 0; 
	public float targetRotation = 0;

	
	void Awake ()
	{
		tunnel = GameObject.FindGameObjectWithTag("Tunnel").transform;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
	
	void FixedUpdate ()
	{
		float rotationAmt = rotationSpeed * Time.deltaTime;
		//if (Input.GetKeyDown (KeyCode.D))
		//{
			if(rotation!= targetRotation) 
			{
				if((rotation-targetRotation) < rotationAmt)
				{
					transform.Rotate(0, 0, rotation-targetRotation);
					rotation += rotation-targetRotation;
				}
				else
				{
					transform.Rotate(0, 0, rotationAmt);
					rotation += rotationAmt;
				}
			}
		//}

		if (Input.GetKeyDown(KeyCode.A))
		{

		}
		
		Vector3 position = transform.position;
		position += direction * speed * Time.deltaTime;
		transform.position = position;
	}
}
