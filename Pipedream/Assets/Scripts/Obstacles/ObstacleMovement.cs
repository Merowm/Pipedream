using UnityEngine;
using System.Collections;

public class ObstacleMovement : MonoBehaviour
{
    public float XAxisSpeedMax = 5.0f;
    public float YAxisSpeedMax = 5.0f;
    public float ZAxisSpeedMax = 5.0f;
    public bool randomizeSpeed = false;
    public float XAxisSpeedMin = -5.0f;
    public float YAxisSpeedMin = -5.0f;
    public float ZAxisSpeedMin = -5.0f;
    public float displayDistance = 400.0f;
    public float backAtOriginDistance = 100.0f;

    private Transform player;
    private MovementForward playerMovement;
    private Vector3 xyzVector;
    private bool triggered = false;
    private Vector3 originalPosition;
    private float speedPerSecond;
    public Vector3 direction;
    private bool positionSet = false;
    public float requiredDistance;

	void Start ()
    {
        if (randomizeSpeed)
        {
            XAxisSpeedMax = Random.Range(XAxisSpeedMin, XAxisSpeedMax + 0.1f);
            YAxisSpeedMax = Random.Range(XAxisSpeedMin, YAxisSpeedMax + 0.1f);
            ZAxisSpeedMax = Random.Range(XAxisSpeedMin, ZAxisSpeedMax + 0.1f);
        }
        xyzVector = new Vector3(XAxisSpeedMax, YAxisSpeedMax, ZAxisSpeedMax);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<MovementForward>();

        originalPosition = transform.position;
        Vector3 nextPosition = originalPosition + xyzVector * Time.deltaTime;

        speedPerSecond = Vector3.Distance(nextPosition,originalPosition) / Time.deltaTime;
        direction = (originalPosition - nextPosition).normalized;
	}

	void FixedUpdate ()
    {
        xyzVector = new Vector3(XAxisSpeedMax, YAxisSpeedMax, ZAxisSpeedMax);

        if (!MovementForward.inHyperSpace)
        {
            //float distance = playerMovement.currentSpeedPerSecond * 3;

            if (displayDistance >= Vector3.Distance(transform.parent.position, player.position))
            {
                triggered = true;

                float distanceToTravelPlayer = displayDistance - backAtOriginDistance;
                float timeItTakes = distanceToTravelPlayer / playerMovement.currentSpeedPerSecond;

                requiredDistance = speedPerSecond * timeItTakes;
                //float direction = (originalPosition - transform.position).normalized;

                SetPosition(requiredDistance);

                if (backAtOriginDistance >= Vector3.Distance(transform.parent.position, player.position))
                {
                    //Debug.Break();
                }

                transform.position += xyzVector * Time.deltaTime;
            }

            if (triggered)
            {
                //transform.position += xyzVector * Time.deltaTime;
            }
        }
        else
        {
            transform.position = originalPosition;
            positionSet = false;
            triggered = false;
        }
	}

    void SetPosition (float distance)
    {
        if (!positionSet)
        {
            transform.position += direction * distance;
            positionSet = true;
        }
    }

    void OnDrawGizmos ()
    {
        Gizmos.DrawLine(transform.position, transform.position + xyzVector * 10.0f);
    }
}
