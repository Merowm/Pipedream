using UnityEngine;
using System.Collections;

public class MovementHorizontal : MonoBehaviour
{
    public float currentRotationSpeed;
    public float hyperspaceSpeed;
    public float spaceSpeed;
    public float maxRotationSpeed;
    public float minSpeed;
    public float hyperspaceAcceleration;
    public float hyperspaceDeceleration;
    public float spaceAcceleration;
    public float spaceDeceleration;
    public float horizontalSpeed;
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
                    currentRotationSpeed += hyperspaceAcceleration * Time.deltaTime;
                }
            }
            //Moves left, slowly accelerating
            else if (Input.GetKey(KeyCode.A))
            {
                if (currentRotationSpeed >= -maxRotationSpeed)
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

            transform.Rotate(Vector3.forward * Time.deltaTime * currentRotationSpeed);
        }
        //Movement while out of hyperspace
        else
        {
            //TODO: acceleration/deceleration
            if (Input.GetKey(KeyCode.D) && transform.position.x - 50 < spaceBoundaries)
            {
                float x = transform.position.x + horizontalSpeed * Time.deltaTime * spaceAcceleration;

                transform.position = new Vector3(x,transform.position.y,transform.position.z);
            }
            if (Input.GetKey(KeyCode.A) && transform.position.x - 50 > -spaceBoundaries)
            {
                transform.position = new Vector3(transform.position.x - horizontalSpeed * Time.deltaTime,
                                                 transform.position.y,
                                                 transform.position.z);
            }
        }
	}
}
