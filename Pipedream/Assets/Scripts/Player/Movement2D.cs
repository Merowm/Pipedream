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
    public float boundaryRadius = 10;

    //Components
    private GameObject mainCamera;
    private Controls controls;
    private Transform shipTransform;
    private PlayerCollisions collisions;
    //In hyperspace variables
    private float hyperspaceAcceleration;
    private float hyperspaceDeceleration;
    private float directionForceRightRotation;
    private float directionForceLeftRotation;
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
        //Components
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        controls = transform.GetComponent<Controls>();
        shipTransform = transform.FindChild("Ship").transform;
        collisions = shipTransform.FindChild("Collider").GetComponent<PlayerCollisions>();

        //Set inHyperspace to false
        MovementForward.inHyperspace = false;
	}

    void Update ()
    {
        hyperspaceAcceleration = maxRotationSpeed * hyperspaceAccelerationMultiplier;
        hyperspaceDeceleration = maxRotationSpeed * hyperspaceDecelerationMultiplier;
        spaceAcceleration = maxSpaceSpeed * spaceAccelerationMultiplier;
        spaceDeceleration = maxSpaceSpeed * spaceDecelerationMultiplier;
    }

	public void MovementUpdate ()
    {
        //Movement while in hypespace
        if (MovementForward.inHyperspace)
        {
            HyperspaceMovement();
        }
        //Movement while out of hyperspace
        else
        {
            //SpaceMovement();
        }
	}

    public void ForcedDodge()
    {
        Controls.controlsActivated = false;

        if (collisions.rightDistance > collisions.leftDistance)
        {
            DodgeRight();
        }
        else if (collisions.rightDistance < collisions.leftDistance)
        {
            DodgeLeft();
        }
        else
        {
            int rand = Random.Range(0, 2);

            if (rand == 0)
            {
                DodgeRight();
            }
            else
            {
                DodgeLeft();
            }
        }

        Controls.controlsActivated = true;
    }

    public void DodgeRight()
    {
        Controls.controlsActivated = false;

        //Dodges right, slowly accelerating, then decelerating when button is released
        directionForceRightRotation = collisions.maxDodgeSpeed;
        directionForceLeftRotation = 0.0f;

        for (float i = directionForceRightRotation; i > 0; i -= collisions.dodgeDeceleration)
        {
            directionForceRightRotation = i;
            //Adds forces from right to those from left and then rotates the player parent accordingly
            currentRotationSpeed = directionForceRightRotation - directionForceLeftRotation;
            transform.Rotate(Vector3.forward * Time.deltaTime * (currentRotationSpeed * 10));
        }
    }

    public void DodgeLeft()
    {
        Controls.controlsActivated = false;
        
        //Dodges left, slowly accelerating, then decelerating when button is released
        directionForceLeftRotation = collisions.maxDodgeSpeed;
        directionForceRightRotation = 0.0f;
        
        for (float i = directionForceLeftRotation; i > 0; i -= collisions.dodgeDeceleration)
        {
            directionForceLeftRotation = i;
            //Adds forces from right to those from left and then rotates the player parent accordingly
            currentRotationSpeed = directionForceRightRotation - directionForceLeftRotation;
            transform.Rotate(Vector3.forward * Time.deltaTime * (currentRotationSpeed * 10));
        }
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

    void HyperspaceMovement()
    {
        //Moves right, slowly accelerating, then decelerating when button is released
        directionForceRightRotation = Movement("Right",
                                               "Left",
                                               directionForceRightRotation,
                                               maxRotationSpeed,
                                               hyperspaceAcceleration,
                                               hyperspaceDeceleration);
        
        //Moves left, slowly accelerating, then decelerating when button is released
        directionForceLeftRotation = Movement("Left",
                                              "Right",
                                              directionForceLeftRotation,
                                              maxRotationSpeed,
                                              hyperspaceAcceleration,
                                              hyperspaceDeceleration);

        //Adds forces from right to those from left and then rotates the player parent accordingly
        currentRotationSpeed = directionForceRightRotation - directionForceLeftRotation;
        transform.Rotate(Vector3.forward * (currentRotationSpeed * 10) * Time.deltaTime);


        /*if (Input.GetButton(KeyCode.T))
        {
            if (shipTransform.localPosition.y < -0.5f)
            {
                shipTransform.localPosition = new Vector3(0, shipTransform.localPosition.y + 0.05f, 0);
            }
            else
            {
                shipTransform.localPosition = new Vector3(0, -0.5f, 0);
            }
        }

        if (Input.GetButton(KeyCode.G))
        {
            if (shipTransform.localPosition.y > -5.0f)
            {
                shipTransform.localPosition = new Vector3(0, shipTransform.localPosition.y - 0.05f, 0);
            }
            else
            {
                shipTransform.localPosition = new Vector3(0, -5.0f, 0);
            }
        }*/
    }

    void SpaceMovement()
    {
        //Moves right, slowly accelerating
        directionForceRight = Movement("Right",
                                       "Left",
                                       directionForceRight,
                                       maxSpaceSpeed,
                                       spaceAcceleration,
                                       spaceDeceleration);

        //Moves left, slowly accelerating
        directionForceLeft = Movement("Left",
                                      "Right",
                                      directionForceLeft,
                                      maxSpaceSpeed,
                                      spaceAcceleration,
                                      spaceDeceleration);

        //Moves up, slowly accelerating
        directionForceUp = Movement("Up",
                                    "Down",
                                    directionForceUp,
                                    maxSpaceSpeed,
                                    spaceAcceleration,
                                    spaceDeceleration);

        //Moves down, slowly accelerating
        directionForceDown = Movement("Down",
                                      "Up",
                                      directionForceDown,
                                      maxSpaceSpeed,
                                      spaceAcceleration,
                                      spaceDeceleration);

        currentSpaceSpeedHorizontal = directionForceRight - directionForceLeft;
        currentSpaceSpeedVertical = directionForceUp - directionForceDown;

        float x = shipTransform.position.x + currentSpaceSpeedHorizontal * Time.deltaTime;
        float y = shipTransform.position.y + currentSpaceSpeedVertical * Time.deltaTime;
        Vector3 xy = new Vector3(x,y,0);
        Vector3 offset = xy - transform.position;

        shipTransform.position = transform.position + Vector3.ClampMagnitude(new Vector3(offset.x,offset.y,0),
                                                                             boundaryRadius);
    }

    float Movement(string button1, string button2, float directionForce, float maxSpeed, float acceleration, float deceleration)
    {
        float d = directionForce;

        //Moves left, slowly accelerating
        if (controls.controls[button1])
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
            if(d > 0 /*&& !controls.controls[button2]Remove !input to change the movement*/)
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
}


















