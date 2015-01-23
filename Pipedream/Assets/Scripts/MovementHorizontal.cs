using UnityEngine;
using System.Collections;

public class MovementHorizontal : MonoBehaviour
{
    //In hyperspace
    public float currentRotationSpeed;
    public float maxRotationSpeed;
    public float hyperspaceAcceleration;
    public float hyperspaceDeceleration;
    //Out of hyperspace
    public float currentHorizontalSpeed;
    public float maxHorizontalSpeed;
    public float spaceAcceleration;
    public float spaceDeceleration;
    public float spaceBoundaries;
    public float rightPower;
    public float leftPower;

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
                if (currentRotationSpeed < maxRotationSpeed)
                {
                    currentRotationSpeed += hyperspaceAcceleration * Time.deltaTime;
                }
            }
            //Moves left, slowly accelerating
            else if (Input.GetKey(KeyCode.A))
            {
                if (currentRotationSpeed > -maxRotationSpeed)
                {
                    currentRotationSpeed -= hyperspaceAcceleration * Time.deltaTime;
                }
            }
            else
            {
                //Slowy decelerates to a halt
                //Right side
                if (currentRotationSpeed > 0)
                {
                    //TODO: Add rounding up/down to zero to eliminate unwanted rotation
                    currentRotationSpeed -= hyperspaceDeceleration * Time.deltaTime;
                }
                //Left side
                else if (currentRotationSpeed < 0)
                {
                    //TODO: Add rounding up/down to zero to eliminate unwanted rotation
                    currentRotationSpeed += hyperspaceDeceleration * Time.deltaTime;
                }
            }
            //Rotates the player inside hyperspace
            transform.Rotate(Vector3.forward * Time.deltaTime * currentRotationSpeed);
        }
        //Movement while out of hyperspace
        else
        {
            //Moves right, slowly accelerating
            if (Input.GetKey(KeyCode.D) && transform.position.x - 50 < spaceBoundaries)
            {
                if (currentHorizontalSpeed < maxHorizontalSpeed)
                {
                    rightPower += spaceAcceleration * Time.deltaTime;
                    //currentHorizontalSpeed += spaceAcceleration * Time.deltaTime;
                }
            }
            else
            {
                if(rightPower >= 0)
                {
                    rightPower -= spaceDeceleration * Time.deltaTime;
                }
                else rightPower = 0;
            }

            //Moves left, slowly accelerating
            if (Input.GetKey(KeyCode.A) && transform.position.x - 50 > -spaceBoundaries)
            {
                if (currentHorizontalSpeed > -maxHorizontalSpeed)
                {
                    leftPower += spaceAcceleration * Time.deltaTime;
                    //currentHorizontalSpeed -= spaceAcceleration * Time.deltaTime;
                }
            }
            else
            {
                if(leftPower >= 0)
                {
                    leftPower -= spaceDeceleration * Time.deltaTime;
                }
                else leftPower = 0;
            }

            //Slowy decelerates to a halt
            /*else
            {
                //Right side
                if (currentHorizontalSpeed > 0)
                {
                    //TODO: Add rounding up/down to zero to eliminate unwanted rotation
                    currentHorizontalSpeed -= spaceDeceleration * Time.deltaTime;
                }
                //Left side
                else if (currentHorizontalSpeed < 0)
                {
                    //TODO: Add rounding up/down to zero to eliminate unwanted rotation
                    currentHorizontalSpeed += spaceDeceleration * Time.deltaTime;
                }
            }*/

            currentHorizontalSpeed += rightPower;

            float x = transform.position.x + currentHorizontalSpeed * Time.deltaTime;
            transform.position = new Vector3(x,transform.position.y,transform.position.z);
        }
	}
}
