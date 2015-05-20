using UnityEngine;
using System.Collections;

public class AsteroidSpin : MonoBehaviour
{
    public Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);
    public float rotationRangeMax = 0.5f;
    public float rotationRangeMin = -0.5f;
    public float displayDistance = 700.0f;
    
    private Transform player;
    private float rotationSpeedX;
    private float rotationSpeedY;
    private float rotationSpeedZ;
    
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rotationSpeedX = Random.Range(rotationRangeMin, rotationRangeMax);
        rotationSpeedY = Random.Range(rotationRangeMin, rotationRangeMax);
        rotationSpeedZ = Random.Range(rotationRangeMin, rotationRangeMax);
        rotation = new Vector3(rotationSpeedX, rotationSpeedY, rotationSpeedZ);
    }
    
    void FixedUpdate ()
    {
        if (displayDistance > Vector3.Distance(new Vector3(0.0f, 0.0f, transform.position.z),
                                               new Vector3(0.0f, 0.0f, player.position.z)))
        {
            transform.Rotate(rotation);
        }
    }
}
