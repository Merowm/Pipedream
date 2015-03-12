using UnityEngine;
using System.Collections;

public class MovementForward : MonoBehaviour
{
    public static bool inHyperSpace = false;
    public bool accelerateToHyperspace = false;
    public bool decelerateToSpaceSpeed = false;
    public float currentSpeed;
    public float currentSpeedPerSecond;
    public float hyperspaceSpeed;
    public float spaceSpeed;
    public float maxSpeed;
    public float minSpeed;
    public float acceleration;
    public float deceleration;
    public Vector3 direction;

    private Movement2D movement2D;
    private SpaceDriveState spaceDriveState;
    private Transform shipTransform;
    private GameObject hyperspaceHorizont;
    private GameObject boundaryCircle;
    private Vector3 originalSpacePosition;
    private Vector3 originalHyperspacePosition;

	void Awake ()
    {
        movement2D = transform.GetComponent<Movement2D>();
        spaceDriveState = transform.GetComponent<SpaceDriveState>();
        shipTransform = transform.FindChild("Ship").transform;
        hyperspaceHorizont = transform.FindChild("HyperspaceHorizont").gameObject;
        boundaryCircle = transform.FindChild("BoundaryCircle").gameObject;
        originalSpacePosition = shipTransform.position;
        originalHyperspacePosition = shipTransform.position + new Vector3(0,-250,0);//shipTransform.position - transform.position;
	}

	void FixedUpdate ()
    {
        if (accelerateToHyperspace)
        {
            AccelerateToHyperspace();
        }
        if (decelerateToSpaceSpeed)
        {
            DecelerateToSpaceSpeed();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //accelerateToHyperspace = true;
        }
        if (Input.GetKey(KeyCode.F))
        {
            //spaceDriveState.DisengagingHyperSpace();
           // decelerateToSpaceSpeed = true;
        }

        /*
        if (currentSpeed >= hyperspaceSpeed && inHyperSpace == false)
        {
            EngagingHyperSpace();
        }
        if (currentSpeed <= hyperspaceSpeed && inHyperSpace == true)
        {
            DisengagingHyperSpace();
        }
        */

        Vector3 lastPosition = transform.position;
        transform.position += direction * currentSpeed * Time.deltaTime;
        Vector3 currentPosition = transform.position;

        currentSpeedPerSecond = (currentPosition.z - lastPosition.z) / Time.deltaTime;
	}

    void AccelerateToHyperspace()
    {
        boundaryCircle.SetActive(false);

        if (currentSpeed < hyperspaceSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = hyperspaceSpeed;
            spaceDriveState.EngagingHyperSpace();
            accelerateToHyperspace = false;
        }
    }
    
    void DecelerateToSpaceSpeed()
    {
        boundaryCircle.SetActive(true);
        hyperspaceHorizont.SetActive(false);

        if (currentSpeed > spaceSpeed)
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = spaceSpeed;
            decelerateToSpaceSpeed = false;
        }
    }
}
