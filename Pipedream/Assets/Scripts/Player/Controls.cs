using UnityEngine;
using System.Collections.Generic;

public class Controls : MonoBehaviour
{
    public static bool controlsActivated = true;
    public Dictionary<string, bool> controls;

    public bool right;
    public bool left;
    public bool up;
    public bool down;

    private Movement2D movement2D;
    private MovementForward movementForward;
    private SpaceDriveState spaceDriveState;

	void Awake ()
    {
        movement2D = transform.GetComponent<Movement2D>();
        movementForward = transform.GetComponent<MovementForward>();
        spaceDriveState = transform.GetComponent<SpaceDriveState>();

        controls = new Dictionary<string, bool>();
        controls.Add("Right", false);
        controls.Add("Left", false);
        controls.Add("Up", false);
        controls.Add("Down", false);
	}

    void Update ()
    {
        //Right
        if (Input.GetAxis("Horizontal") == 1)
        {
            controls["Right"] = true;
        }
        else
        {
            controls["Right"] = false;
        }
        //Left
        if (Input.GetAxis("Horizontal") == -1)
        {
            controls["Left"] = true;
        }
        else
        {
            controls["Left"] = false;
        }
        //Up
        if (Input.GetAxis("Vertical") == 1)
        {
            controls["Up"] = true;
        }
        else
        {
            controls["Up"] = false;
        }
        //Down
        if (Input.GetAxis("Vertical") == -1)
        {
            controls["Down"] = true;
        }
        else
        {
            controls["Down"] = false;
        }

        if (controlsActivated)
        {
            right = controls ["Right"];
            left = controls ["Left"];
            up = controls ["Up"];
            down = controls ["Down"];
        }
        else
        {
            right = controls ["Right"] = false;
            left = controls ["Left"] = false;
            up = controls ["Up"] = false;
            down = controls ["Down"] = false;
        }

        TemporaryControls();
    }
	
	void FixedUpdate ()
    {
        movement2D.MovementUpdate();
    }

    void TemporaryControls ()
    {
        //Next drive state
        if (Input.GetKeyDown(KeyCode.E) && (movementForward.currentSpeed == movementForward.spaceSpeed ||
                                            movementForward.currentSpeed == movementForward.hyperspaceSpeed))
        {
            if (!MovementForward.inHyperSpace)
            {
                if (spaceDriveState.currentDriveIndex < spaceDriveState.driveStatePositions.Count - 1)
                {
                    spaceDriveState.SetDriveStateForward();
                    movementForward.accelerateToHyperspace = true;
                }
                else
                {
                    spaceDriveState.SetDriveStateForward();
                    spaceDriveState.DisengagingHyperSpace();
                    movementForward.decelerateToSpaceSpeed = true;
                }
            }
            else
            {
                spaceDriveState.SetDriveStateForward();
                spaceDriveState.DisengagingHyperSpace();
                movementForward.decelerateToSpaceSpeed = true;
            }
        }
        //Previous drive state
        if (Input.GetKeyDown(KeyCode.Q) && (movementForward.currentSpeed == movementForward.spaceSpeed ||
                                            movementForward.currentSpeed == movementForward.hyperspaceSpeed))
        {
            if (!MovementForward.inHyperSpace)
            {
                if (spaceDriveState.currentDriveIndex > 0)
                {
                    spaceDriveState.SetDriveStateBackward();
                    movementForward.accelerateToHyperspace = true;
                }
                else
                {
                    spaceDriveState.SetDriveStateBackward();
                    spaceDriveState.DisengagingHyperSpace();
                    movementForward.decelerateToSpaceSpeed = true;
                }
            }
            else
            {
                spaceDriveState.SetDriveStateBackward();
                spaceDriveState.DisengagingHyperSpace();
                movementForward.decelerateToSpaceSpeed = true;
            }
        }
    }
}