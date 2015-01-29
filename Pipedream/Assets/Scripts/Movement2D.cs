using UnityEngine;
using System.Collections;

public class Movement2D : MonoBehaviour
{
    //In hyperspace variables
    public float currentRotationSpeed = 0;
    public float maxRotationSpeed = 10;
    public float hyperspaceAccelerationMultiplier = 2;
    public float hyperspaceDecelerationMultiplier = 3;
    //Out of hyperspace variables
    public float currentSpaceSpeedHorizontal = 0;
    public float currentSpaceSpeedVertical = 0;
    public float maxSpaceSpeed = 10;
    public float spaceAccelerationMultiplier = 2;
    public float spaceDecelerationMultiplier = 3;
    public float spaceBoundaries = 10;

    //Components
    private GameObject mainCamera;
    private MovementForward movementForward;
    private Transform shipTransform;
    //In hyperspace variables
    private float hyperspaceAcceleration;
    private float hyperspaceDeceleration;
    public float directionForceRightRotation;
    public float directionForceLeftRotation;
    //Out of hyperspace variables
    private float spaceAcceleration;
    private float spaceDeceleration;
    private float directionForceRight;
    private float directionForceLeft;
    private float directionForceUp;
    private float directionForceDown;
    private float distanceFromShipToParent;

	void Awake ()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        movementForward = transform.GetComponent<MovementForward>();
        shipTransform = transform.GetChild(0).transform;
	}

    void Update ()
    {
        hyperspaceAcceleration = maxRotationSpeed * hyperspaceAccelerationMultiplier;
        hyperspaceDeceleration = maxRotationSpeed * hyperspaceDecelerationMultiplier;
        spaceAcceleration = maxSpaceSpeed * spaceAccelerationMultiplier;
        spaceDeceleration = maxSpaceSpeed * spaceDecelerationMultiplier;
    }

	void FixedUpdate ()
    {
        //Movement while in hypespace
        if (movementForward.inHyperSpace)
        {
            HyperspaceMovement();
        }
        //Movement while out of hyperspace
        else
        {
            SpaceMovement();
        }
	}

    void HyperspaceMovement()
    {
        //Moves right, slowly accelerating, then decelerating when button is released
        directionForceRightRotation = Movement(KeyCode.D,
                                               KeyCode.A,
                                               directionForceRightRotation,
                                               maxRotationSpeed,
                                               hyperspaceAcceleration,
                                               hyperspaceDeceleration);
        
        //Moves left, slowly accelerating, then decelerating when button is released
        directionForceLeftRotation = Movement(KeyCode.A,
                                              KeyCode.D,
                                              directionForceLeftRotation,
                                              maxRotationSpeed,
                                              hyperspaceAcceleration,
                                              hyperspaceDeceleration);

        currentRotationSpeed = directionForceRightRotation - directionForceLeftRotation;
        transform.Rotate(Vector3.forward * Time.deltaTime * (currentRotationSpeed * 10));
    }

    void SpaceMovement()
    {
        //MovementWithMouse();

        //Moves right, slowly accelerating
        directionForceRight = Movement(KeyCode.D,
                                       KeyCode.A,
                                       directionForceRight,
                                       maxSpaceSpeed,
                                       spaceAcceleration,
                                       spaceDeceleration);
        
        //Moves left, slowly accelerating
        directionForceLeft = Movement(KeyCode.A,
                                      KeyCode.D,
                                      directionForceLeft,
                                      maxSpaceSpeed,
                                      spaceAcceleration,
                                      spaceDeceleration);

        //Moves up, slowly accelerating
        directionForceUp = Movement(KeyCode.W,
                                    KeyCode.S,
                                    directionForceUp,
                                    maxSpaceSpeed,
                                    spaceAcceleration,
                                    spaceDeceleration);

        //Moves down, slowly accelerating
        directionForceDown = Movement(KeyCode.S,
                                      KeyCode.W,
                                      directionForceDown,
                                      maxSpaceSpeed,
                                      spaceAcceleration,
                                      spaceDeceleration);

        currentSpaceSpeedHorizontal = directionForceRight - directionForceLeft;
        currentSpaceSpeedVertical = directionForceUp - directionForceDown;
        
        float x = shipTransform.position.x + currentSpaceSpeedHorizontal * Time.deltaTime;
        float y = shipTransform.position.y + currentSpaceSpeedVertical * Time.deltaTime;
        shipTransform.position = new Vector3(x,y,shipTransform.position.z);
    }

    public void ResetVariables()
    {
        //In hyperspace variables
        currentRotationSpeed = 0;
        directionForceRightRotation = 0;
        directionForceLeftRotation = 0;
        //Out of hyperspace variables
        currentSpaceSpeedHorizontal = 0;
        currentSpaceSpeedVertical = 0;
        directionForceRight = 0;
        directionForceLeft = 0;
        directionForceUp = 0;
        directionForceDown = 0;
    }

    float Movement(KeyCode button1, KeyCode button2, float direction, float maxSpeed, float acceleration, float deceleration)
    {
        float d = direction;

        //Moves left, slowly accelerating
        if (Input.GetKey(button1))
        {
            if (d < maxSpeed)
            {
                d += acceleration * Time.deltaTime;
                return d;
            }
            else
            {
                d = maxSpeed;
                return maxSpeed;
            }
        }
        else
        {
            //Deceleration after releasing the button
            if(d > 0 && !Input.GetKey(button2)/*Remove !input to change the movement*/)
            {
                d -= deceleration * Time.deltaTime;
                return d;
            }
            else
            {
                d = 0;
                return d;
            }
        }
    }
    //TODO:Currently inverted
    void MovementWithMouse()
    {
        /*
        float speed = 5;
        Vector3 direction;
        Vector3 position = shipTransform.position;
        Vector3 mousePos = Input.mousePosition;
        Vector3 ray = mainCamera.camera.ScreenToWorldPoint(new Vector3(Screen.width - mousePos.x,
                                                                       Screen.height - mousePos.y,
                                                                       mainCamera.GetComponent<CameraFollow>().distanceFromTarget));



        direction = (ray - position).normalized;
        position += direction * speed * Time.deltaTime;
        shipTransform.position = Vector3.Lerp(shipTransform.position, position, speed);
        */
    }
}
