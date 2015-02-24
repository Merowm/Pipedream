using UnityEngine;
using System.Collections;

public class ObstacleMovementNEW : MonoBehaviour
{
    public Vector3 direction = new Vector3(1.0f,1.0f,1.0f);
    //public float XAxisSpeedMax = 5.0f;
    //public float YAxisSpeedMax = 5.0f;
    //public float ZAxisSpeedMax = 5.0f;
    public bool randomizeSpeed = false;
    public float speed = 5.0f;
    //public float XAxisSpeedMin = -5.0f;
    //public float YAxisSpeedMin = -5.0f;
    //public float ZAxisSpeedMin = -5.0f;
    public float displayDistance = 400.0f;
    public float backAtOriginDistance = 100.0f;

    private Transform origin;
    private Transform player;
    private MovementForward playerMovement;
    //private Vector3 xyzVector;
    private Vector3 originalPosition;
    private float speedPerSecond;
    private bool positionSet = false;
    private float requiredDistance;
    private Vector3 requiredPosition;
    
    void Start ()
    {
        origin = transform.parent.FindChild("Origin").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<MovementForward>();

        origin.position = transform.position;

        if (randomizeSpeed)
        {
            //XAxisSpeedMax = Random.Range(XAxisSpeedMin, XAxisSpeedMax + 0.1f);
            //YAxisSpeedMax = Random.Range(XAxisSpeedMin, YAxisSpeedMax + 0.1f);
            //ZAxisSpeedMax = Random.Range(XAxisSpeedMin, ZAxisSpeedMax + 0.1f);
        }
        //xyzVector = new Vector3(XAxisSpeedMax, YAxisSpeedMax, ZAxisSpeedMax);
        direction = direction.normalized;
        
        originalPosition = transform.position;
        Vector3 nextPosition = originalPosition + direction * speed * Time.deltaTime;
        
        speedPerSecond = Vector3.Distance(nextPosition,originalPosition) / Time.deltaTime;
        direction = (originalPosition - nextPosition).normalized;

        //JATKA TÄSTÄ, PLAYER SPEED PER SECOND
        float distanceToTravelPlayer = displayDistance - backAtOriginDistance;
        float timeItTakes = distanceToTravelPlayer / playerMovement.currentSpeedPerSecond;
        requiredDistance = speedPerSecond * timeItTakes;

        if (!positionSet)
        {
            requiredPosition = GetRequiredPosition(requiredDistance);
            transform.position = requiredPosition;
            positionSet = true;
        }
    }
    
    void FixedUpdate ()
    {
        //xyzVector = new Vector3(XAxisSpeedMax, YAxisSpeedMax, ZAxisSpeedMax);
        direction = direction.normalized;
        
        if (!MovementForward.inHyperSpace)
        {
            if (!positionSet)
            {
                requiredPosition = GetRequiredPosition(requiredDistance);
                transform.position = requiredPosition;
                positionSet = true;
            }
            
            if (displayDistance >= Vector3.Distance(transform.parent.position, player.position))
            {
                //float direction = (originalPosition - transform.position).normalized;
                
                if (backAtOriginDistance >= Vector3.Distance(transform.parent.position, player.position))
                {
                    //Debug.Break();
                }
                
                transform.position += direction * speed * Time.deltaTime;
            }
        }
        else
        {
            transform.position = originalPosition;
            positionSet = false;
        }
    }
    
    Vector3 GetRequiredPosition (float distance)
    {
        return transform.position - direction * distance;
    }
    
    void OnDrawGizmos ()
    {
        //Gizmos.DrawLine(transform.position, transform.position + xyzVector * 10.0f);
        if (origin != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, origin.position);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, requiredPosition);
            Gizmos.color = Color.white;
        }
    }
}
