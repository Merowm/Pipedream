using UnityEngine;
using System.Collections;

public class AsteroidSpin : MonoBehaviour
{
    public float rotationRangeMax = 0.03f;
    public float rotationRangeMin = -0.03f;

    private Vector3 rotation;

	void Update ()
    {
        float rotationSpeedX = Random.Range(rotationRangeMin, rotationRangeMax);
        float rotationSpeedY= Random.Range(rotationRangeMin, rotationRangeMax);
        float rotationSpeedZ = Random.Range(rotationRangeMin, rotationRangeMax);

        rotation = new Vector3(rotation.x + rotationSpeedX, rotation.y + rotationSpeedY, rotation.z + rotationSpeedZ);
        transform.Rotate(rotation);
	}
}
