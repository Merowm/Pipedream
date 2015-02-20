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

    private Transform origin;
    private Transform player;
    private MovementForward playerMovement;
    private float speed;
    private Vector3 originalPosition;
    private float speedPerSecond;
    private bool positionSet = false;
    private float requiredDistance;
    private float distanceToTravelPlayer;
    private float timeItTakes;
    private float playerSpeedPerSecond;

	void Start ()
    {
        origin = transform.parent.FindChild("Origin").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<MovementForward>();
        
        //Player's speed per second calculations
        Vector3 playerOriginalPosition = player.position;
        Vector3 playerNextPosition = playerOriginalPosition + playerMovement.direction *
            playerMovement.spaceSpeed * Time.deltaTime;
        playerSpeedPerSecond = Vector3.Distance(playerNextPosition,playerOriginalPosition) / Time.deltaTime;
        
        //Setting of direction and speed
        if (randomizeSpeed)
        {
            //Randomly sets values for speed and direction
            speedMax = Random.Range(speedMin, speedMax + 0.1f);
            direction = Vector3.Normalize(new Vector3(Random.Range(-5, 5),
                                                      Random.Range(-5, 5),
                                                      Random.Range(-5, 5)));
        }
        else
        {
            //Calculates direction based on pre set values and keeps speed at it's pre set value
            direction = (transform.position - origin.position).normalized;
        }
        speed = speedMax;
        
        //Obstacle's speed per second calculations
        originalPosition = transform.position;
        Vector3 nextPosition = originalPosition + direction * speed * Time.deltaTime;
        speedPerSecond = Vector3.Distance(nextPosition,originalPosition) / Time.deltaTime;
        
        //Setting the obstacle at a specific distance from its origin,
        //so that by the time the player's z-axis is the same as the obstacles,
        //the obstacle will be at its origin position.
        requiredDistance = GetRequiredDistance();
        SetPosition(requiredDistance);
    }

	void FixedUpdate ()
    {
        //direction = direction.normalized;
        speed = speedMax;

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
        }
	}

    float GetRequiredDistance()
    {
        distanceToTravelPlayer = displayDistance - backAtOriginDistance + 80;
        timeItTakes = distanceToTravelPlayer / playerSpeedPerSecond;
        
        return speedPerSecond * timeItTakes;
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
        /*Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direction * requiredDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + -direction * requiredDistance);
        Gizmos.color = Color.white;*/
        if (origin != null)
        {
            Gizmos.DrawLine(transform.position, origin.position);
        }
    }
}
