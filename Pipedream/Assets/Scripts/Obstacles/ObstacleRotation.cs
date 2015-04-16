using UnityEngine;
using System.Collections;

public class ObstacleRotation : MonoBehaviour
{
    public Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);
    public bool randomizeRotation = false;
    public float rotationRangeMax = 3.0f;
    public float rotationRangeMin = -3.0f;
    public float displayDistance = 400.0f;

    private Transform player;
    private float rotationSpeedX;
    private float rotationSpeedY;
    private float rotationSpeedZ;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (randomizeRotation)
        {
            rotationSpeedX = Random.Range(rotationRangeMin, rotationRangeMax);
            rotationSpeedY = Random.Range(rotationRangeMin, rotationRangeMax);
            rotationSpeedZ = Random.Range(rotationRangeMin, rotationRangeMax);
            rotation = new Vector3(0.0f, 0.0f, rotationSpeedZ);
        }
    }

	void Update ()
    {
        if (displayDistance > Vector3.Distance(new Vector3(0.0f, 0.0f, transform.position.z),
                                               new Vector3(0.0f, 0.0f, player.position.z)))
        {
            transform.Rotate(rotation * 75 * Time.deltaTime);
        }
	}
}
