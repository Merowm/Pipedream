using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour
{
	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Obstacle")
		{
			Debug.Log("collision");
			Debug.Break();
		}
	}
}
