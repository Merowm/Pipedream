using UnityEngine;
using System.Collections;

public class MoveAround : MonoBehaviour {

	public Vector3 gravityDirection = new Vector3(0,-1,0);
	public Vector2 gravityAngle = new Vector3(0,0,0);
	public float angleChangeInDegrees;
	public float ang;
		// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//GetDirectionFromInput ();
		//rigidbody.AddRelativeForce (gravityDirection);

	}
	void FixedUpdate()
	{
		GetDirectionFromInput ();
		rigidbody.AddForce (gravityDirection);

		}
	void GetDirectionFromInput()
	{
		if (Input.GetKey(KeyCode.A))
		{ Debug.Log ("A pressed");
			gravityDirection = deltaDirection (-1);
		}
		if (Input.GetKey (KeyCode.D)) 
		{ Debug.Log ("D pressed");
			gravityDirection = deltaDirection(1);
		}
	}
	Vector3 deltaDirection(float rotationDirection)
	{
		float deltaAngle = Mathf.Deg2Rad * angleChangeInDegrees * rotationDirection * Time.deltaTime;
		Vector3 result = new Vector3();
		result.x = gravityDirection.x * Mathf.Cos(deltaAngle) - 
			gravityDirection.y * Mathf.Sin(deltaAngle);
		result.y = gravityDirection.x * Mathf.Sin(deltaAngle) +
			gravityDirection.y * Mathf.Cos(deltaAngle);
		result.z = gravityDirection.z;
		//rigidbody.MoveRotation(new Quaternion(0, 0, PlayerAngle(), 0));

		return result;
		}
	float PlayerAngle()
	{
		float result = Mathf.Atan2 (gravityDirection.x, gravityDirection.y);
		result = Mathf.Rad2Deg * result;
		ang = result;
		return result;
	}
}
