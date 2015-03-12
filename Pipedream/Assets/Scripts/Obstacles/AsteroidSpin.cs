using UnityEngine;
using System.Collections;

public class AsteroidSpin : MonoBehaviour
{
    public float rotationRangeMax = 0.03f;
    public float rotationRangeMin = -0.03f;
    public float displayDistance = 400.0f;

    private Transform player;
    private Vector3 rotation;
    private float rotationSpeedX;
    private float rotationSpeedY;
    private float rotationSpeedZ;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rotationSpeedX = Random.Range(rotationRangeMin, rotationRangeMax);
        rotationSpeedY = Random.Range(rotationRangeMin, rotationRangeMax);
        rotationSpeedZ = Random.Range(rotationRangeMin, rotationRangeMax);
    }

	void Update ()
    {
        if (displayDistance > Vector3.Distance(new Vector3(0,0,transform.parent.position.z),
                                               new Vector3(0,0,player.position.z)))
        {
            rotation = new Vector3(rotationSpeedX, rotationSpeedY, rotationSpeedZ);
            transform.Rotate(rotation);
        }
	}
}
