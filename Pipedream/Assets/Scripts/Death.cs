using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour
{
	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Obstacle")
		{
			Application.LoadLevel(0);
			Debug.Log("collision");
			//Debug.Break();
		}
	}
}
