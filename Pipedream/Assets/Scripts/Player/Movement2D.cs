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

    public float currentSpaceSpeedMouse = 0;
    public float angle = 0;

    //Components
    private GameObject mainCamera;
    private Controls controls;
    private MovementForward movementForward;
    private Transform shipTransform;
    private Transform mouseAnglePoint;
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
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        controls = transform.GetComponent<Controls>();
        movementForward = transform.GetComponent<MovementForward>();
        shipTransform = transform.FindChild("Ship").transform;
        mouseAnglePoint = transform.FindChild("MouseAnglePoint").transform;
        //Hides the mouse cursor
        //Screen.showCursor = false;
	}

    void Update ()
    {
        hyperspaceAcceleration = maxRotationSpeed * hyperspaceAccelerationMultiplier;
        hyperspaceDeceleration = maxRotationSpeed * hyperspaceDecelerationMultiplier;
        spaceAcceleration = maxSpaceSpeed * spaceAccelerationMultiplier;
        spaceDeceleration = maxSpaceSpeed * spaceDecelerationMultiplier;
    }

	public void KeyboardControls ()
    {
        //Movement while in hypespace
        if (movementForward.inHyperSpace)
        {
            HyperspaceMovement("Keyboard");
        }
        //Movement while out of hyperspace
        else
        {
            SpaceMovement("Keyboard");
        }
	}

    public void MouseControls ()
    {
        //Movement while in hypespace
        if (movementForward.inHyperSpace)
        {
            HyperspaceMovement("Mouse");
        }
        //Movement while out of hyperspace
        else
        {
            SpaceMovement("Mouse");
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
        currentSpaceSpeedMouse = 0;
    }

    void HyperspaceMovement(string controls)
    {
        switch(controls)
        {
            case "Keyboard":
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
                break;

            case "Mouse":
                MovementWithMouseHyperspace();
                break;
        }

        //Adds forces from right to those from left and then rotates the player parent accordingly
        currentRotationSpeed = directionForceRightRotation - directionForceLeftRotation;
        transform.Rotate(Vector3.forward * Time.deltaTime * (currentRotationSpeed * 10));
    }

    void SpaceMovement(string controls)
    {
        switch (controls)
        {
            case "Keyboard":
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
                shipTransform.position = new Vector3(x, y, shipTransform.position.z);
                break;

            case "Mouse":
                MovementWithMouseSpace();
                break;
        }
    }

    float Movement(string button1, string button2, float direction, float maxSpeed, float acceleration, float deceleration)
    {
        float d = direction;

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
            if(d > 0 && !controls.controls[button2]/*Remove !input to change the movement*/)
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

    void MovementWithMouseSpace()
    {
        Vector3 direction;
        Vector3 position = shipTransform.position;
        Vector3 mousePos = mainCamera.camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                            Input.mousePosition.y,
                                                                            mainCamera.GetComponent<CameraFollow>().distanceFromTarget));

        float distance = Vector3.Distance(shipTransform.position, mousePos);

        if (distance > 2)
        {
            if (currentSpaceSpeedMouse < maxSpaceSpeed)
            {
                currentSpaceSpeedMouse += spaceAcceleration * Time.deltaTime;
            }
            else currentSpaceSpeedMouse = maxSpaceSpeed;
        }
        else
        {
            if(currentSpaceSpeedMouse > 0)
            {
                currentSpaceSpeedMouse -= spaceDeceleration * Time.deltaTime;
            }
        }

        direction = (mousePos - position).normalized;
        position += direction * currentSpaceSpeedMouse * Time.deltaTime;
        shipTransform.position = Vector3.Lerp(shipTransform.position, position, currentSpaceSpeedMouse);
    }

    void MovementWithMouseHyperspace()
    {
        Vector3 mousePos = mainCamera.camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                            Input.mousePosition.y,
                                                                            mainCamera.GetComponent<CameraFollow>().distanceFromTarget));
        Vector3 directionMouseToParent = (mousePos - transform.position).normalized;
        Vector3 directionShipToParent = (mouseAnglePoint.position - transform.position).normalized;

        angle = Vector3.Angle(directionMouseToParent, directionShipToParent);

        if (angle >= 100 || angle <= 80)
        {
            if (angle < 80)
            {
                if (directionForceRightRotation < maxRotationSpeed)
                {
                    directionForceRightRotation += hyperspaceAcceleration * Time.deltaTime;
                }
                else directionForceRightRotation = maxRotationSpeed;
            }
            else
            {
                if(directionForceRightRotation > 0)
                {
                    directionForceRightRotation -= hyperspaceDeceleration * Time.deltaTime;
                }
                else directionForceRightRotation = 0;
            }

            if (angle > 100)
            {
                if (directionForceLeftRotation < maxRotationSpeed)
                {
                    directionForceLeftRotation += hyperspaceAcceleration * Time.deltaTime;
                }
                else directionForceLeftRotation = maxRotationSpeed;
            }
            else
            {
                if(directionForceLeftRotation > 0)
                {
                    directionForceLeftRotation -= hyperspaceDeceleration * Time.deltaTime;
                }
                else directionForceLeftRotation = 0;
            }
        }
        else
        {
            if(directionForceRightRotation > 0)
            {
                directionForceRightRotation -= hyperspaceDeceleration * Time.deltaTime;
            }
            else directionForceRightRotation = 0;

            if(directionForceLeftRotation > 0)
            {
                directionForceLeftRotation -= hyperspaceDeceleration * Time.deltaTime;
            }
            else directionForceLeftRotation = 0;
        }
    }
}


















