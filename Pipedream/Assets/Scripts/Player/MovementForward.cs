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
    public float countdownToHyperspace = 5.0f;
    public float accelerationTime = 1.2f;
    public float decelerationTime = 1.2f;
    public Vector3 direction;

    private SpaceDriveState spaceDriveState;
    private GameObject hyperspaceHorizont;
    private GameObject boundaryCircle;
    private float acceleration; //How many floats need to be added to currentSpeed
                                //every second to achieve hyperspaceSpeed in accelerationTime (seconds)
    private float deceleration; //How many floats need to be detracted from currentSpeed
                                //every second to achieve hyperspaceSpeed in decelerationTime (seconds)

	void Awake ()
    {
        spaceDriveState = transform.GetComponent<SpaceDriveState>();
        hyperspaceHorizont = transform.FindChild("HyperspaceHorizont").gameObject;
        boundaryCircle = transform.FindChild("BoundaryCircle").gameObject;

        acceleration = (hyperspaceSpeed - spaceSpeed) / accelerationTime;
        deceleration = (hyperspaceSpeed - spaceSpeed) / decelerationTime;
	}

	void FixedUpdate ()
    {
        if (accelerateToHyperspace)
        {
            countdownToHyperspace -= Time.deltaTime;

            if (countdownToHyperspace <= accelerationTime)
            {
                AccelerateToHyperspace();
            }
        }
        if (decelerateToSpaceSpeed)
        {
            DecelerateToSpaceSpeed();
        }

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
        } else
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
