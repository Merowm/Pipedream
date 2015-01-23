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

    public bool stopInitialAcceleration = false;
    private Transform mainCamera;

	void Awake ()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}

	void FixedUpdate ()
    {
        if (!stopInitialAcceleration)
        {
            InitialAcceleration();
           // Debug.Log("asd");
        }

        if (Input.GetKey(KeyCode.W) && currentSpeed < maxSpeed)
        {
            //currentSpeed += acceleration * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && currentSpeed > minSpeed)
        {
            //currentSpeed -= deceleration * Time.deltaTime;
        }

        //AccelerateToHyperspace();

        if (Input.GetKey(KeyCode.F))
        {
            DecelerateToSpaceSpeed();
        }

        if (currentSpeed >= hyperspaceSpeed && inHyperSpace == false)
        {
            EngagingHyperSpace();
        }
        if (currentSpeed <= hyperspaceSpeed && inHyperSpace == true)
        {
            DisengagingHyperSpace();
        }

        transform.position += direction * currentSpeed * Time.deltaTime;
	}

    //Accelerates to hyperspace at start of the level
    void InitialAcceleration()
    {
        if (currentSpeed < hyperspaceSpeed)
        {
            //TODO: Automated acceleration
            if (Input.GetKey(KeyCode.W))
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
        }
        else
        {
            stopInitialAcceleration = true;
            //EnteringHyperSpace();
        }
        //transform.position += direction * currentSpeed * Time.deltaTime;
    }

    void EngagingHyperSpace()
    {
        inHyperSpace = true;
        transform.rotation = new Quaternion(0,0,0,0);
        transform.position = new Vector3(0,0,0);
        mainCamera.transform.position = new Vector3(transform.position.x,
                                                    mainCamera.transform.position.y,
                                                    mainCamera.transform.position.z);
    }
    
    void DisengagingHyperSpace()
    {
        DecelerateToSpaceSpeed();
        inHyperSpace = false;
        transform.rotation = new Quaternion(0,0,0,0);
        transform.position = new Vector3(50,0,0);
        mainCamera.transform.position = new Vector3(transform.position.x,
                                                    mainCamera.transform.position.y,
                                                    mainCamera.transform.position.z);
    }

    void AccelerateToHyperspace()
    {
        if (currentSpeed < hyperspaceSpeed)
        {
            //TODO: Automated acceleration
            if (Input.GetKey(KeyCode.W))
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
        }
    }

    void DecelerateToSpaceSpeed()
    {
        if (currentSpeed > spaceSpeed)
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }
    }
}
