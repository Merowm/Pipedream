using UnityEngine;
using System.Collections;

public class MovementHorizontal : MonoBehaviour
{
    public float currentRotationSpeed;
    public float maxRotationSpeed;
    public float hyperspaceAcceleration;
    public float hyperspaceDeceleration;

    public double currentHorizontalSpeed;
    public double maxHorizontalSpeed;
    public double spaceAcceleration;
    public double spaceDeceleration;

    public float spaceBoundaries;

    private MovementForward movementForward;

	void Start ()
    {
        movementForward = transform.GetComponent<MovementForward>();
	}

	void FixedUpdate ()
    {
        //Movement while in hypespace
        if (movementForward.inHyperSpace)
        {
            //Moves right, slowly accelerating
            if (Input.GetKey(KeyCode.D))
            {
                if (currentRotationSpeed <= maxRotationSpeed)
                {
                    //currentRotationSpeed += hyperspaceAcceleration * Time.deltaTime;
                    currentRotationSpeed += maxRotationSpeed * hyperspaceAcceleration;
                }
            }
            //Moves left, slowly accelerating
            else if (Input.GetKey(KeyCode.A))
            {
                if (currentRotationSpeed >= -maxRotationSpeed)
                {
                    //currentRotationSpeed -= hyperspaceAcceleration * Time.deltaTime;
                    currentRotationSpeed -= maxRotationSpeed * hyperspaceAcceleration;
                }
            }
            else
            {
                //Slowy decelerates to a halt
                //Right side
                if (currentRotationSpeed > 0.0f)
                {
                    //TODO: Add rounding up/down to zero to eliminate unwanted rotation
                    //currentRotationSpeed -= hyperspaceDeceleration * Time.deltaTime;
                    currentRotationSpeed -= maxRotationSpeed * hyperspaceDeceleration;
                }
                //Left side
                else if (currentRotationSpeed < 0.0f)
                {
                    //TODO: Add rounding up/down to zero to eliminate unwanted rotation
                    //currentRotationSpeed += hyperspaceDeceleration * Time.deltaTime;
                    currentRotationSpeed += maxRotationSpeed * hyperspaceDeceleration;
                }
            }
            //Executes the rotation moving the player's ship
            transform.Rotate(Vector3.forward * Time.deltaTime * currentRotationSpeed);
        }
        //Movement while out of hyperspace
        else
        {
            //TODO: acceleration/deceleration
            if (Input.GetKey(KeyCode.D) && transform.position.x - 50 < spaceBoundaries)
            {
                if (currentHorizontalSpeed <= maxHorizontalSpeed)
                {
                    currentHorizontalSpeed += spaceAcceleration;// * Time.deltaTime;
                }
            }
            else if (Input.GetKey(KeyCode.A) && transform.position.x - 50 > -spaceBoundaries)
            {
                if (currentHorizontalSpeed >= -maxHorizontalSpeed)
                {
                    currentHorizontalSpeed -= spaceAcceleration;// * Time.deltaTime;
                }
            }
            else
            {
                if (currentHorizontalSpeed > 0)
                {
                    //TODO: Add rounding up/down to zero to eliminate unwanted rotation
                    currentHorizontalSpeed -= spaceDeceleration;// * Time.deltaTime;
                }
                else if (currentHorizontalSpeed < 0)
                {
                    //TODO: Add rounding up/down to zero to eliminate unwanted rotation
                    currentHorizontalSpeed += spaceDeceleration;// * Time.deltaTime;
                }
            }

            float x = transform.position.x + (float)currentHorizontalSpeed * Time.deltaTime;
            transform.position = new Vector3(x,transform.position.y,transform.position.z);
        }
	}
}
