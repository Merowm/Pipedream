using UnityEngine;
using System.Collections;

public class ObjectRotation : MonoBehaviour
{
    public float rotationSpeed = 2.5f;
    public bool triWall = false;

    public float rotatedAmount = 0.0f;
    private enum ROTATE{ positive, negative };
    private ROTATE currentState = ROTATE.positive;

    void Start()
    {
        rotationSpeed *= MovementForward.difficultyMultiplier;
    }

	void FixedUpdate ()
    {
        //Rotation for everything but TriWall
        if (!triWall)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed) * 75 * Time.deltaTime);
        }
        //Rotation for TriWall
        else
        {
            switch(currentState)
            {
                case ROTATE.positive:
                    transform.Rotate(new Vector3(0, 0, rotationSpeed) * 75 * Time.deltaTime);

                    if (rotationSpeed > 0)
                    {
                        rotatedAmount = transform.rotation.z;
                    }
                    else rotatedAmount = -transform.rotation.z;

                    if (rotatedAmount >= 1)
                    {
                        currentState = ROTATE.negative;
                    }
                    break;

                case ROTATE.negative:
                    transform.Rotate(new Vector3(0, 0, -rotationSpeed) * 75 * Time.deltaTime);

                    if (rotationSpeed > 0)
                    {
                        rotatedAmount = transform.rotation.z;
                    }
                    else rotatedAmount = -transform.rotation.z;

                    if (rotatedAmount <= 0)
                    {
                        currentState = ROTATE.positive;
                    }
                    break;
            }
        }
	}
}
