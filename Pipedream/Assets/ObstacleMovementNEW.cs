using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ObstacleMovementNEW : MonoBehaviour
{
    public Vector3 direction = new Vector3(1.0f,1.0f,1.0f);
    public bool usingDirectionBased = false;
    public bool randomizeSpeed = false;
    public float speed = 5.0f;
    public float displayDistance = 400.0f;
    public float backAtOriginDistance = 0.0f;

    private Transform startingPositionObjectTransform;
    private Transform player;
    private MovementForward playerMovement;
    private Vector3 startingPosition;
    private float speedPerSecond;
    private float timeItTakes;
    private bool positionSet = false;
    private Vector3 requiredPosition;
    private float obstacleTravelingDistance;

    void Start ()
    {
        startingPositionObjectTransform = transform.parent.FindChild("StartingPosition").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<MovementForward>();

        startingPosition = startingPositionObjectTransform.position;

        if (randomizeSpeed)
        {
            //XAxisSpeedMax = Random.Range(XAxisSpeedMin, XAxisSpeedMax + 0.1f);
            //YAxisSpeedMax = Random.Range(XAxisSpeedMin, YAxisSpeedMax + 0.1f);
            //ZAxisSpeedMax = Random.Range(XAxisSpeedMin, ZAxisSpeedMax + 0.1f);
        }
        //xyzVector = new Vector3(XAxisSpeedMax, YAxisSpeedMax, ZAxisSpeedMax);
        direction = direction.normalized;

        //UNFINISHED
        float distanceToTravelPlayer = displayDistance - backAtOriginDistance;
        timeItTakes = distanceToTravelPlayer / playerMovement.currentSpeedPerSecond;

        if (usingDirectionBased)
        {
            Vector3 nextPosition = startingPosition + direction * speed * Time.deltaTime;
        
            speedPerSecond = Vector3.Distance(nextPosition, startingPosition) / Time.deltaTime;
            direction = (startingPosition - nextPosition).normalized;

            obstacleTravelingDistance = speedPerSecond * timeItTakes;
        }
        else
        {
            obstacleTravelingDistance = Vector3.Distance(startingPosition,transform.parent.position);
            speedPerSecond = obstacleTravelingDistance / timeItTakes;
            speed = speedPerSecond / 2;
            direction = (transform.parent.position - startingPosition).normalized;
        }
    }

    void Update ()
    {
        /*
        #if UNITY_EDITOR
        if(!Application.isPlaying)
        {
            direction = direction.normalized;
            
            startingPosition = transform.position;
            Vector3 nextPosition = startingPosition + direction * speed * Time.deltaTime;
            
            speedPerSecond = Vector3.Distance(nextPosition,startingPosition) / Time.deltaTime;
            direction = (startingPosition - nextPosition).normalized;
            
            //UNFINISHED
            float distanceToTravelPlayer = displayDistance - backAtOriginDistance;
            timeItTakes = distanceToTravelPlayer / playerMovement.currentSpeedPerSecond;
            obstacleTravelingDistance = speedPerSecond * timeItTakes;
        }
        #endif
        */
    }
    
    void FixedUpdate ()
    {
        //xyzVector = new Vector3(XAxisSpeedMax, YAxisSpeedMax, ZAxisSpeedMax);
        direction = direction.normalized;
        
        if (!MovementForward.inHyperSpace)
        {
            if (displayDistance >= Vector3.Distance(new Vector3(0,0,transform.parent.position.z),
                                                    new Vector3(0,0,player.position.z)))
            {
                SetPosition();

                if (transform.parent.position.z >= player.position.z - 50.0f)
                {
                    transform.position += direction * speedPerSecond * Time.deltaTime;
                }
            }
        }
        else
        {
            transform.position = transform.parent.position;
            positionSet = false;
        }
    }
    
    void SetPosition ()
    {
        if (!positionSet)
        {
            if (usingDirectionBased)
            {
                requiredPosition = transform.position - direction * obstacleTravelingDistance;
            }
            else requiredPosition = startingPosition;

            transform.position = requiredPosition;
            startingPosition = transform.position;
            positionSet = true;
        }
    }
    
    void OnDrawGizmos ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.parent.position);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, startingPosition);
        Gizmos.color = Color.white;
    }
}
