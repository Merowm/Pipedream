using UnityEngine;
using System.Collections;

//TODO: Delete this script when MovementForward and MovementHorizontal are done
public class Movement : MonoBehaviour
{
	public float currentSpeed;
    public float hyperspaceSpeed;
    public float spaceSpeed;
	public float maxSpeed;
	public float minSpeed;
	public float acceleration;
	public float deceleration;
	public Vector3 direction;
	public float circularSpeed = 100f;
    public float horizontalSpeed;
    public float spaceBoundaries = 5f;
    public bool inHyperSpace = false;

    public bool stopInitialAcceleration = false;
	private Transform mainCamera;

	void Awake ()
	{
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}

	void FixedUpdate ()
	{
        while (!stopInitialAcceleration)
        {
            InitialAcceleration();
            Debug.Log("asd");
        }

        if (inHyperSpace)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * circularSpeed);
                //mainCamera.transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(-Vector3.forward * Time.deltaTime * circularSpeed);
                //mainCamera.transform.Rotate(-Vector3.forward * Time.deltaTime * rotationSpeed);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.D) && transform.position.x - 50 < spaceBoundaries)
            {
                transform.position = new Vector3(transform.position.x + horizontalSpeed * Time.deltaTime,
                                                 transform.position.y,
                                                 transform.position.z);
            }
            if (Input.GetKey(KeyCode.A) && transform.position.x - 50> -spaceBoundaries)
            {
                transform.position = new Vector3(transform.position.x - horizontalSpeed * Time.deltaTime,
                                                 transform.position.y,
                                                 transform.position.z);
            }
        }

        if (Input.GetKey(KeyCode.W) && currentSpeed < maxSpeed)
		{
            currentSpeed += acceleration * Time.deltaTime;
		}
        if (Input.GetKey(KeyCode.S) && currentSpeed > minSpeed)
		{
            currentSpeed -= deceleration * Time.deltaTime;
		}

        if (currentSpeed <= minSpeed)
        {
           // EnteringHyperSpace();
        }
        else if (currentSpeed >= maxSpeed)
        {
           // ExitingHyperSpace();
        }
        transform.position += direction * currentSpeed * Time.deltaTime;
	}

    void EnteringHyperSpace()
    {
        inHyperSpace = true;
        transform.rotation = new Quaternion(0,0,0,0);
        transform.position = new Vector3(0,0,0);
        mainCamera.transform.position = new Vector3(transform.position.x,
                                                    mainCamera.transform.position.y,
                                                    mainCamera.transform.position.z);
    }
    
    void ExitingHyperSpace()
    {
        inHyperSpace = false;
        transform.rotation = new Quaternion(0,0,0,0);
        transform.position = new Vector3(50,0,0);
        mainCamera.transform.position = new Vector3(transform.position.x,
                                                    mainCamera.transform.position.y,
                                                    mainCamera.transform.position.z);
    }

    void InitialAcceleration()
    {
        if (currentSpeed < hyperspaceSpeed && (Input.GetKey(KeyCode.W)))
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            stopInitialAcceleration = true;
            EnteringHyperSpace();
        }
        transform.position += direction * currentSpeed * Time.deltaTime;
    }
}
