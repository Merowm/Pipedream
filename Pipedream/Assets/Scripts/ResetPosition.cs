using UnityEngine;
using System.Collections;

public class ResetPosition : MonoBehaviour
{
    public float resettingPosition = -1000.0f;

    private GameObject tunnel;

    void Awake ()
    {
        tunnel = GameObject.FindGameObjectWithTag("MovingHyperPart");
    }

	void Update ()
    {
        if (transform.position.z >= resettingPosition)
        {
            transform.position = new Vector3(transform.position.x,
                                             transform.position.y,
                                             transform.position.z - resettingPosition);
            tunnel.transform.position = new Vector3(tunnel.transform.position.x,
                                                    tunnel.transform.position.y,
                                                    tunnel.transform.position.z - resettingPosition);
        }
	}
}
