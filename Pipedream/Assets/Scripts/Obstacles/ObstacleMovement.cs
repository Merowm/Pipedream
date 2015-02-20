using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class ObstacleMovement : MonoBehaviour
{
    public Vector3 direction = new Vector3(1.0f,1.0f,1.0f);
    public float speedMax = 5.0f;
    public bool randomizeSpeed = false;
    public float speedMin = 0.1f;
    public float displayDistance = 400.0f;
    public float backAtOriginDistance = 100.0f;

    private Transform player;
    private MovementForward playerMovement;
    private float speed;
    private bool triggered = false;
    private Vector3 originalPosition;
    private float speedPerSecond;
    private bool positionSet = false;
    private float requiredDistance;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<MovementForward>();

        if (randomizeSpeed)
        {
            speedMax = Random.Range(speedMin, speedMax + 0.1f);
            direction = Vector3.Normalize(new Vector3(Random.Range(-5, 5),
                                                      Random.Range(-5, 5),
                                                      Random.Range(-5, 5)));
        }
        direction = direction.normalized;
        speed = speedMax;
        originalPosition = transform.position;
        Vector3 nextPosition = originalPosition + direction * speed * Time.deltaTime;
        speedPerSecond = Vector3.Distance(nextPosition,originalPosition) / Time.deltaTime;
	}

	void FixedUpdate ()
    {
        direction = direction.normalized;
        speed = speedMax;

        //direction = transform

        float distanceToTravelPlayer = displayDistance - backAtOriginDistance;
        float timeItTakes = distanceToTravelPlayer / playerMovement.currentSpeedPerSecond;
        
        requiredDistance = speedPerSecond * timeItTakes;

        SetPosition(requiredDistance);

        if (backAtOriginDistance >= Vector3.Distance(transform.parent.position, player.position))
        {
            //Debug.Break();
        }

        if (!MovementForward.inHyperSpace)
        {
            if (displayDistance >= Vector3.Distance(transform.parent.position, player.position))
            {
                transform.position += direction * speed * Time.deltaTime;
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
            transform.position -= direction * distance;
            positionSet = true;
        }
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direction * requiredDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + -direction * requiredDistance);
        Gizmos.color = Color.white;
    }
}
