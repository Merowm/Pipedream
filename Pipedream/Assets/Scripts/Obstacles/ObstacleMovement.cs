using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ObstacleMovement : MonoBehaviour
{
    public Vector3 direction = new Vector3(1.0f,1.0f,1.0f);
    public bool randomizeSpeed = false;
    public float speed = 5.0f;
    public float displayDistance = 400.0f;
    public float backAtOriginDistance = 0.0f;
    public Transform startingPosition;

    private Transform player;
    private MovementForward playerMovement;
    public float speedPerSecond;
    public float timeItTakes;
    private bool positionSet = false;
    private Vector3 requiredPosition;
    public float obstacleTravelingDistance;

    void Start ()
    {
        //startingPosition = transform.parent.FindChild("StartingPosition").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<MovementForward>();

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
        Debug.Log(timeItTakes, this.gameObject);

        if (transform.parent.tag == "SpawnedObject")
        {
            Vector3 nextPosition = startingPosition.position + direction * speed * Time.deltaTime;
        
            speedPerSecond = Vector3.Distance(nextPosition, startingPosition.position) / Time.deltaTime;
            direction = (startingPosition.position - nextPosition).normalized;

            obstacleTravelingDistance = speedPerSecond * timeItTakes;

            startingPosition.position = transform.position - direction * obstacleTravelingDistance;
        }
        else
        {
            obstacleTravelingDistance = Vector3.Distance(startingPosition.position,transform.parent.position);
            speedPerSecond = obstacleTravelingDistance / 10;//timeItTakes;
            speed = speedPerSecond / 2;
            direction = (transform.parent.position - startingPosition.position).normalized;
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
            if (transform.parent.tag == "SpawnedObject")
            {
                requiredPosition = transform.position - direction * obstacleTravelingDistance;
            }
            else requiredPosition = startingPosition.position;

            transform.position = requiredPosition;
            startingPosition.position = transform.position;
            positionSet = true;
        }
    }
    
    void OnDrawGizmos ()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.parent.position);

        if (positionSet)
        {
            Gizmos.color = Color.red;
        }
        else Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, startingPosition.position);

        Gizmos.color = Color.white;
    }
}
