using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

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
#if UNITY_ANDROID
        //if user is touching the screen
        if (Input.touchCount > 0)
        {
            //check first touch (prevent issues with multi touch)
            Touch touch = Input.touches[0];
            int pointerID = touch.fingerId;
            //if user is not touching a button (eg. pause button)
            if (!EventSystem.current.IsPointerOverGameObject(pointerID))
            {
                //if user is touching left of screen
                if (touch.position.x >= 0 && touch.position.x <= Screen.width * 0.5)
                {
                    controls["Left"] = true;
                    controls["Right"] = false;
                    controls["Up"] = false;
                    controls["Down"] = false;
                }
                //if user is touching right of screen
                else
                {
                    controls["Right"] = true;
                    controls["Left"] = false;
                    controls["Up"] = false;
                    controls["Down"] = false;
                }                
            }
        }
        else
        {
            controls["Left"] = false;
            controls["Right"] = false;
            controls["Up"] = false;
            controls["Down"] = false;
        }
#else
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
#endif

        if (controlsActivated)
        {
            //tested
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
            if (!MovementForward.inHyperspace)
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
            if (!MovementForward.inHyperspace)
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