using UnityEngine;
using System.Collections;

public class MovementForward : MonoBehaviour
{
    public static bool inHyperspace = true;
    public static float difficultyMultiplier = 0.75f;
    public bool accelerateToHyperspace = false;
    public bool decelerateToSpaceSpeed = false;
    public float currentSpeed;
    public float currentSpeedPerSecond;
    public float hyperspaceSpeed;
    public float spaceSpeed;
    public float maxSpeed;
    public float minSpeed;
    public float countdownToHyperspace = 5.0f;
    public float accelerationTime = 1.2f;
    public float decelerationTime = 1.2f;
    public Vector3 direction;

    private SpaceDriveState spaceDriveState;
    private float acceleration; //How many floats need to be added to currentSpeed
                                //every second to achieve hyperspaceSpeed in accelerationTime (seconds)
    private float deceleration; //How many floats need to be detracted from currentSpeed
                                //every second to achieve hyperspaceSpeed in decelerationTime (seconds)

    //for testing
    public bool forceDifficulty = false;
    public Difficulty.DIFFICULTY forcedDifficulty;

	void Awake ()
    {
        spaceDriveState = transform.GetComponent<SpaceDriveState>();

        if (Difficulty.currentDifficulty == Difficulty.DIFFICULTY.beginner)
        {
            difficultyMultiplier = 0.75f;
        }
        else if (Difficulty.currentDifficulty == Difficulty.DIFFICULTY.normal)
        {
            difficultyMultiplier = 1.0f;
        }
        //for testing
        if (forceDifficulty)
        {
            switch(forcedDifficulty)
            {
                case Difficulty.DIFFICULTY.beginner:
                    difficultyMultiplier = 0.75f;
                    break;
                case Difficulty.DIFFICULTY.normal:
                    difficultyMultiplier = 1.0f;
                    break;
            }
        }

        hyperspaceSpeed *= difficultyMultiplier;

        acceleration = (hyperspaceSpeed - spaceSpeed) / accelerationTime;
        deceleration = (hyperspaceSpeed - spaceSpeed) / decelerationTime;
	}

	void FixedUpdate ()
    {
        if (accelerateToHyperspace)
        {
            countdownToHyperspace -= Time.deltaTime;

            if (countdownToHyperspace <= accelerationTime)
            {
                AccelerateToHyperspace();
            }
        }
        if (decelerateToSpaceSpeed)
        {
            DecelerateToSpaceSpeed();
        }

        Vector3 lastPosition = transform.position;
        transform.position += direction * currentSpeed * Time.deltaTime;
        Vector3 currentPosition = transform.position;

        currentSpeedPerSecond = (currentPosition.z - lastPosition.z) / Time.deltaTime;
	}

    void AccelerateToHyperspace()
    {
        if (currentSpeed < hyperspaceSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        } else
        {
            currentSpeed = hyperspaceSpeed;
            spaceDriveState.EngagingHyperSpace();
            accelerateToHyperspace = false;
        }
    }
    
    void DecelerateToSpaceSpeed()
    {
        if (currentSpeed > spaceSpeed)
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = spaceSpeed;
            decelerateToSpaceSpeed = false;
        }
    }
}
