using UnityEngine;
using System.Collections;

public class MovementForward : MonoBehaviour
{
    public float currentSpeed;
    public float hyperspaceSpeed;
    public float spaceSpeed;
    public float maxSpeed;
    public float minSpeed;
    public float acceleration;
    public float deceleration;
    public bool inHyperSpace = false;
    public Vector3 direction;

    private bool accelerateToHyperspace = false;
    private bool decelerateToSpaceSpeed = false;
    private Movement2D movement2D;
    private Transform mainCamera;
    private Transform shipTransform;
    private Vector3 originalSpacePosition;
    private Vector3 originalHyperspacePosition;

	void Awake ()
    {
        movement2D = transform.GetComponent<Movement2D>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        shipTransform = transform.GetChild(0).transform;
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

        transform.position += direction * currentSpeed * Time.deltaTime;
	}

    void EngagingHyperSpace()
    {
        inHyperSpace = true;
        transform.rotation = new Quaternion(0,0,0,0);
        transform.position = new Vector3(0,0,0);
        shipTransform.position = originalHyperspacePosition;
        mainCamera.transform.position = new Vector3(transform.position.x,
                                                    mainCamera.transform.position.y,
                                                    mainCamera.transform.position.z);
        mainCamera.rotation = new Quaternion(0,0,0,0);
        movement2D.ResetVariables();
    }
    
    void DisengagingHyperSpace()
    {
        inHyperSpace = false;
        transform.rotation = new Quaternion(0,0,0,0);
        transform.position = new Vector3(50,0,0);
        shipTransform.position = originalSpacePosition;
        mainCamera.transform.position = new Vector3(transform.position.x,
                                                    mainCamera.transform.position.y,
                                                    mainCamera.transform.position.z);
        mainCamera.rotation = new Quaternion(0,0,0,0);
        movement2D.ResetVariables();
    }

    void AccelerateToHyperspace()
    {
        if (currentSpeed < hyperspaceSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            EngagingHyperSpace();
            accelerateToHyperspace = false;
        }
    }

    void DecelerateToSpaceSpeed()
    {
        if (currentSpeed > spaceSpeed)
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }
        else
        {
            decelerateToSpaceSpeed = false;
        }
    }
}
