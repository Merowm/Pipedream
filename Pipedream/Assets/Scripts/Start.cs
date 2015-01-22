using UnityEngine;
using System.Collections;

public class Start : MonoBehaviour
{
	void Update ()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			Application.LoadLevel(1);
		}
	}
}
