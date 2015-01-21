using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour
{
	public float gravityPower = 1;

	void FixedUpdate ()
	{
		Vector3 direction = (transform.position - transform.parent.position).normalized;
		Physics.gravity = new Vector3 (direction.x * gravityPower, direction.y * gravityPower, 0);
	}
}
