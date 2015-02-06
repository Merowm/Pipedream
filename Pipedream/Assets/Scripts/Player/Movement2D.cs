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

    public float currentSpaceSpeedMouse = 0;
    //public float angle = 0;

    //Components
    private GameObject mainCamera;
    private Controls controls;
    private MovementForward movementForward;
    private Transform shipTransform;
    private Transform mouseAnglePointParent;
    private Transform mouseAnglePointHorizontal;
    private Transform mouseAnglePointVertical;
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
        //shipTransform.position = new Vector3(50,0,0);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        controls = transform.GetComponent<Controls>();
        movementForward = transform.GetComponent<MovementForward>();
        shipTransform = transform.FindChild("Ship").transform;
        mouseAnglePointParent = transform.FindChild("MouseAnglePoint").transform;
        mouseAnglePointHorizontal = shipTransform.FindChild("MouseAnglePointHorizontal").transform;
        mouseAnglePointVertical = shipTransform.FindChild("MouseAnglePointVertical").transform;
        //baseSpaceBoundariesMax = spaceBoundariesMax;
        //baseSpaceBoundariesMin = spaceBoundariesMin;
        //Hides the mouse cursor
        //Screen.showCursor = false;
	}

    void Start()
    {

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
                Vector3 xy = new Vector3(x,y,0);
                Vector3 offset = xy - transform.position;

                shipTransform.position = transform.position + Vector3.ClampMagnitude(new Vector3(offset.x,offset.y,0),boundaryRadius);
                break;

            case "Mouse":
                MovementWithMouseSpace();
                /*
                currentSpaceSpeedHorizontal = directionForceRight - directionForceLeft;
                currentSpaceSpeedVertical = directionForceUp - directionForceDown;
                
                float xa = shipTransform.position.x + currentSpaceSpeedHorizontal * Time.deltaTime;
                float ya = shipTransform.position.y + currentSpaceSpeedVertical * Time.deltaTime;
                
                xa = Mathf.Clamp(xa,spaceBoundariesMin.x,spaceBoundariesMax.x);
                ya = Mathf.Clamp(ya,spaceBoundariesMin.y,spaceBoundariesMax.y);
                
                shipTransform.position = new Vector3(xa, ya, shipTransform.position.z);*/
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
                //d = maxSpeed;
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
                //d = 0;
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
                                                                            mainCamera.transform.parent.GetComponent<CameraFollow>().distanceFromTarget));
        float distance = Vector3.Distance(shipTransform.position, mousePos);

        if (distance > 0.5f)
        {
            if (currentSpaceSpeedMouse < maxSpaceSpeed)
            {
                currentSpaceSpeedMouse += spaceAcceleration * Time.deltaTime;
            }
            else currentSpaceSpeedMouse = maxSpaceSpeed;
        }
        else
        {
            if (distance > 0.1f)
            {
                if(currentSpaceSpeedMouse > 0)
                {
                    currentSpaceSpeedMouse -= spaceDeceleration * Time.deltaTime;
                }
            }
            else currentSpaceSpeedMouse = 0;
        }

        direction = (mousePos - position).normalized;
        position += direction * currentSpaceSpeedMouse * Time.deltaTime;
        Vector3 offset = position - transform.position;

        position = transform.position + Vector3.ClampMagnitude(new Vector3(offset.x,offset.y,0),boundaryRadius);
        shipTransform.position = Vector3.Lerp(shipTransform.position, position, currentSpaceSpeedMouse);

        //WORK IN PROGRESS:
        /*
        Vector3 mousePos = mainCamera.camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                            Input.mousePosition.y,
                                                                            mainCamera.GetComponent<CameraFollow>().distanceFromTarget));
        float distance = Vector3.Distance(shipTransform.position, mousePos);

        if (distance > 0.1f)
        {
            directionForceRight = MouseAngler(shipTransform,
                                          mouseAnglePointHorizontal,
                                          112.5f,
                                          67.5f,
                                          directionForceRight,
                                          directionForceLeft,
                                          maxSpaceSpeed,
                                          spaceAcceleration,
                                          spaceDeceleration,
                                          1);
            directionForceLeft = MouseAngler(shipTransform,
                                         mouseAnglePointHorizontal,
                                         112.5f,
                                         67.5f,
                                         directionForceRight,
                                         directionForceLeft,
                                         maxSpaceSpeed,
                                         spaceAcceleration,
                                         spaceDeceleration,
                                         2);
            directionForceUp = MouseAngler(shipTransform,
                                       mouseAnglePointVertical,
                                       112.5f,
                                       67.5f,
                                       directionForceUp,
                                       directionForceDown,
                                       maxSpaceSpeed,
                                       spaceAcceleration,
                                       spaceDeceleration,
                                       1);
            directionForceDown = MouseAngler(shipTransform,
                                         mouseAnglePointVertical,
                                         112.5f,
                                         67.5f,
                                         directionForceUp,
                                         directionForceDown,
                                         maxSpaceSpeed,
                                         spaceAcceleration,
                                         spaceDeceleration,
                                         2);
        }
        else
        {
            if (distance > 0.1f)
            {
                if(currentSpaceSpeedMouse > 0)
                {
                    currentSpaceSpeedMouse -= spaceDeceleration * Time.deltaTime;
                }
            }
            else directionForceRight = directionForceLeft = directionForceLeft = directionForceDown = 0;
        }
        */
    }

    void MovementWithMouseHyperspace()
    {
        directionForceRightRotation = MouseAngler(transform,
                                                  mouseAnglePointParent,
                                                  95,
                                                  85,
                                                  directionForceRightRotation,
                                                  directionForceLeftRotation,
                                                  maxRotationSpeed,
                                                  hyperspaceAcceleration,
                                                  hyperspaceDeceleration,
                                                  1);
        directionForceLeftRotation = MouseAngler(transform,mouseAnglePointParent,
                                                 95,
                                                 85,
                                                 directionForceRightRotation,
                                                 directionForceLeftRotation,
                                                 maxRotationSpeed,
                                                 hyperspaceAcceleration,
                                                 hyperspaceDeceleration,
                                                 2);
    }

    float MouseAngler(Transform centerPoint, Transform anglePoint, float angleUpperValue, float angleLowerValue,
                      float directionForce1, float directionForce2, float maxSpeed, float acceleration,
                      float deceleration, uint directionForceToBeReturned)
    {
        Vector3 mousePos = mainCamera.camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                            Input.mousePosition.y,
                                                                            mainCamera.transform.parent.GetComponent<CameraFollow>().distanceFromTarget));
        Vector3 directionMouseToParent = (mousePos - centerPoint.position).normalized;
        Vector3 directionAnglePointToParent = (anglePoint.position - centerPoint.position).normalized;
        
        float angle = Vector3.Angle(directionMouseToParent, directionAnglePointToParent);
        
        if (angle >= angleUpperValue || angle <= angleLowerValue)
        {
            if (angle < angleLowerValue)
            {
                if (directionForce1 < maxSpeed)
                {
                    directionForce1 += acceleration * Time.deltaTime;
                }
                else directionForce1 = maxSpeed;
            }
            else
            {
                if(directionForce1 > 0)
                {
                    directionForce1 -= deceleration * Time.deltaTime;
                }
                else directionForce1 = 0;
            }
            
            if (angle > angleUpperValue)
            {
                if (directionForce2 < maxSpeed)
                {
                    directionForce2 += acceleration * Time.deltaTime;
                }
                else directionForce2 = maxSpeed;
            }
            else
            {
                if(directionForce2 > 0)
                {
                    directionForce2 -= deceleration * Time.deltaTime;
                }
                else directionForce2 = 0;
            }
        }
        else
        {
            if(directionForce1 > 0)
            {
                directionForce1 -= deceleration * Time.deltaTime;
            }
            else directionForce1 = 0;
            
            if(directionForce2 > 0)
            {
                directionForce2 -= deceleration * Time.deltaTime;
            }
            else directionForce2 = 0;
        }

        if (directionForceToBeReturned == 1)
        {
            return directionForce1;
        }
        else
        {
            return directionForce2;
        }
    }
}


















