using UnityEngine;
using System.Collections;

public class HyperTunnelMovement : MonoBehaviour
{
    public float currentSpeed = 100.0f;
    public float distanceReset = 500.0f;
    public Vector3 direction = new Vector3(0,0,-1);

    private Transform player;

	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update ()
    {
        transform.position += direction * currentSpeed * Time.deltaTime;

        if (player.position.z >= transform.position.z + distanceReset)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z + distanceReset);
        }
	}
}