using UnityEngine;
using System.Collections;

public class MovementForward : MonoBehaviour
{
    public static bool inHyperSpace = false;
    public float currentSpeed;
    public float currentSpeedPerSecond;
    public float hyperspaceSpeed;
    public float spaceSpeed;
    public float maxSpeed;
    public float minSpeed;
    public float acceleration;
    public float deceleration;
    public Vector3 direction;
    public bool accelerateToHyperspace = false;
    public bool decelerateToSpaceSpeed = false;

    private Movement2D movement2D;
    private Transform shipTransform;
    private GameObject hyperspaceHorizont;
    private GameObject boundaryCircle;
    private Vector3 originalSpacePosition;
    private Vector3 originalHyperspacePosition;

	void Awake ()
    {
        movement2D = transform.GetComponent<Movement2D>();
        shipTransform = transform.FindChild("Ship").transform;
        hyperspaceHorizont = transform.FindChild("HyperspaceHorizont").gameObject;
        boundaryCircle = transform.FindChild("BoundaryCircle").gameObject;
        originalSpacePosition = shipTransform.position;
        originalHyperspacePosition = shipTransform.position - transform.position;
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
            accelerateToHyperspace = true;
        }
        if (Input.GetKey(KeyCode.F))
        {
            DisengagingHyperSpace();
            decelerateToSpaceSpeed = true;
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

    public void DisengagingHyperSpace()
    {
        inHyperSpace = false;
        transform.rotation = new Quaternion(0,0,0,0);
        transform.position = new Vector3(0,0,0);
        shipTransform.position = originalSpacePosition;
        //mainCamera.transform.position = new Vector3(transform.position.x,
        //                                            mainCamera.transform.position.y,
        //                                            mainCamera.transform.position.z);
        //mainCamera.rotation = new Quaternion(0,0,0,0);
        movement2D.ResetVariables();
    }

    void EngagingHyperSpace()
    {
        inHyperSpace = true;
        transform.rotation = new Quaternion(0,0,0,0);
        transform.position = new Vector3(0,-250,0);
        shipTransform.position = originalHyperspacePosition;
        //mainCamera.transform.position = new Vector3(transform.position.x,
        //                                            mainCamera.transform.position.y,
        //                                            mainCamera.transform.position.z);
        //mainCamera.rotation = new Quaternion(0,0,0,0);
        movement2D.ResetVariables();
        hyperspaceHorizont.SetActive(true);
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
            EngagingHyperSpace();
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
