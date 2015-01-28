using UnityEngine;
using System.Collections;

public class MovementHorizontal : MonoBehaviour
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
    private MovementForward movementForward;
    private Transform shipTransform;
    //In hyperspace variables
    private float hyperspaceAcceleration;
    private float hyperspaceDeceleration;
    private float rightRotation;
    private float leftRotation;
    //Out of hyperspace variables
    private float spaceAcceleration;
    private float spaceDeceleration;
    private float rightPower;
    private float leftPower;
    private float upPower;
    private float downPower;
    private float distanceFromShipToParent;

	void Awake ()
    {
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
        //Moves right, slowly accelerating
        if (Input.GetKey(KeyCode.D))
        {
            if (rightRotation < maxRotationSpeed)
            {
                rightRotation += hyperspaceAcceleration * Time.deltaTime;
            }
            else rightRotation = maxRotationSpeed;
        }
        else
        {
            //Deceleration after releasing the button
            if(rightRotation > 0 && !Input.GetKey(KeyCode.A)/*Remove !input to change the movement*/)
            {
                rightRotation -= hyperspaceDeceleration * Time.deltaTime;
            }
            else rightRotation = 0;
        }
        
        //Moves left, slowly accelerating
        if (Input.GetKey(KeyCode.A))
        {
            if (leftRotation < maxRotationSpeed)
            {
                leftRotation += hyperspaceAcceleration * Time.deltaTime;
            }
            else leftRotation = maxRotationSpeed;
        }
        else
        {
            //Deceleration after releasing the button
            if(leftRotation > 0 && !Input.GetKey(KeyCode.D)/*Remove !input to change the movement*/)
            {
                leftRotation -= hyperspaceDeceleration * Time.deltaTime;
            }
            else leftRotation = 0;
        }

        currentRotationSpeed = rightRotation - leftRotation;
        transform.Rotate(Vector3.forward * Time.deltaTime * (currentRotationSpeed * 10));
    }

    void SpaceMovement()
    {
        //Moves right, slowly accelerating
        if (Input.GetKey(KeyCode.D))
        //if (Input.GetKey(KeyCode.D))
        {
            if (rightPower < maxSpaceSpeed)
            {
                rightPower += spaceAcceleration * Time.deltaTime;
            }
            else rightPower = maxSpaceSpeed;
        }
        else
        {
            //Deceleration after releasing the button
            if(rightPower > 0 && !Input.GetKey(KeyCode.A))
            {
                rightPower -= spaceDeceleration * Time.deltaTime;
            }
            else rightPower = 0;
        }
        
        //Moves left, slowly accelerating
        if (Input.GetKey(KeyCode.A))
        //if (Input.GetKey(KeyCode.A))
        {
            if (leftPower < maxSpaceSpeed)
            {
                leftPower += spaceAcceleration * Time.deltaTime;
            }
            else leftPower = maxSpaceSpeed;
        }
        else
        {
            //Deceleration after releasing the button
            if(leftPower > 0 && !Input.GetKey(KeyCode.D))
            {
                leftPower -= spaceDeceleration * Time.deltaTime;
            }
            else leftPower = 0;
        }

        //Moves up, slowly accelerating
        if (Input.GetKey(KeyCode.W))
        {
            if (upPower < maxSpaceSpeed)
            {
                upPower += spaceAcceleration * Time.deltaTime;
            }
            else upPower = maxSpaceSpeed;
        }
        else
        {
            //Deceleration after releasing the button
            if(upPower > 0 && !Input.GetKey(KeyCode.S))
            {
                upPower -= spaceDeceleration * Time.deltaTime;
            }
            else upPower = 0;
        }

        //Moves down, slowly accelerating
        if (Input.GetKey(KeyCode.S))
        {
            if (downPower < maxSpaceSpeed)
            {
                downPower += spaceAcceleration * Time.deltaTime;
                //currentHorizontalSpeed += spaceAcceleration * Time.deltaTime;
            }
            else downPower = maxSpaceSpeed;
        }
        else
        {
            //Deceleration after releasing the button
            if(downPower > 0 && !Input.GetKey(KeyCode.W))
            {
                downPower -= spaceDeceleration * Time.deltaTime;
            }
            else downPower = 0;
        }

        currentSpaceSpeedHorizontal = rightPower - leftPower;
        currentSpaceSpeedVertical = upPower - downPower;
        
        float x = shipTransform.position.x + currentSpaceSpeedHorizontal * Time.deltaTime;
        float y = shipTransform.position.y + currentSpaceSpeedVertical * Time.deltaTime;
        shipTransform.position = new Vector3(x,y,shipTransform.position.z);
    }

    public void ResetVariables()
    {
        //In hyperspace variables
        currentRotationSpeed = 0;
        rightRotation = 0;
        leftRotation = 0;
        //Out of hyperspace variables
        currentSpaceSpeedHorizontal = 0;
        currentSpaceSpeedVertical = 0;
        rightPower = 0;
        leftPower = 0;
        upPower = 0;
        downPower = 0;
    }
}
