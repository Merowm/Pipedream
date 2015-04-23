using UnityEngine;
using System.Collections;

public class HyperTunnelMovement : MonoBehaviour
{
    public float speed = 100.0f;
    public float speedPerSecond = 0.0f;
    public float distanceReset = 500.0f;
    public Vector3 direction = new Vector3(0,0,-1);
    public bool infinite = false;
    public Vector3 infiniteStart;
    public bool customization = false;
    public Vector3 resettingPosition = new Vector3(0,0,0);

    private Transform player;

	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        resettingPosition = new Vector3(0,0,distanceReset);
	}

	void FixedUpdate ()
    {
        Vector3 lastPosition = transform.position;
        transform.position += direction * speed * Time.deltaTime;
        Vector3 currentPosition = transform.position;

        speedPerSecond = (currentPosition.z - lastPosition.z) / Time.deltaTime;

        if (!infinite)
        {
            if (!customization)
            {
                if (player.position.z >= transform.position.z + distanceReset)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + distanceReset);
                }
            }
            else
            {
                if (transform.position.z <= -distanceReset)
                {
                    transform.position = resettingPosition;

                    if (transform.GetComponent<CustomizationSpawner>() != null)
                    {
                        transform.GetComponent<CustomizationSpawner>().ClearObjects();
                        transform.GetComponent<CustomizationSpawner>().SpawnObjects();
                    }
                }
            }
        }
        else
        {
            if (infiniteStart.z >= transform.position.z + distanceReset)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + distanceReset);
            }
        }
	}
}