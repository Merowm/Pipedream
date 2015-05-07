using UnityEngine;
using System.Collections;

public class ObjectRotation : MonoBehaviour
{
    public float rotationSpeed = 2.5f;
    public bool triWall = false;

	void FixedUpdate ()
    {
        //Rotation for everything but TriWall
        if (!triWall)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed) * 75 * Time.deltaTime);
        }
        //Rotation for TriWall
        else
        {

        }
	}
}
