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

    private SpaceDriveState spaceDriveState;
    private GameObject hyperspaceHorizont;
    private GameObject boundaryCircle;

	void Awake ()
    {
        spaceDriveState = transform.GetComponent<SpaceDriveState>();
        hyperspaceHorizont = transform.FindChild("HyperspaceHorizont").gameObject;
        boundaryCircle = transform.FindChild("BoundaryCircle").gameObject;
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
