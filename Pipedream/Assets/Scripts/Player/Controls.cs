using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Controls : MonoBehaviour
{
    public static bool controlsActivated = true;
    public Dictionary<string, bool> controls;

    public bool right;
    public bool left;

    private Movement2D movement2D;
    private MovementForward movementForward;
    private SpaceDriveState spaceDriveState;
    private Inventory inventory;

	void Awake ()
    {
        movement2D = transform.GetComponent<Movement2D>();
        movementForward = transform.GetComponent<MovementForward>();
        spaceDriveState = transform.GetComponent<SpaceDriveState>();
        inventory = transform.GetComponentInChildren<Inventory>();

        controls = new Dictionary<string, bool>();
        controls.Add("Right", false);
        controls.Add("Left", false);
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
                }
                //if user is touching right of screen
                else
                {
                    controls["Right"] = true;
                    controls["Left"] = false;
                }                
            }
        }
        else
        {
            controls["Left"] = false;
            controls["Right"] = false;
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
        //Input for items
        //Repair
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //inventory.UseItem(0);
        }
        //Invulnerability
        if (Input.GetButtonDown("Fire1"))
        {
            inventory.UseItem(1);
        }
        //Time slow
        if (Input.GetButtonDown("Fire2"))
        {
            inventory.UseItem(2);
        }
#endif

        if (controlsActivated)
        {
            //tested
            right = controls ["Right"];
            left = controls ["Left"];
        }
        else
        {
            right = controls ["Right"] = false;
            left = controls ["Left"] = false;
        }
    }
	
	void FixedUpdate ()
    {
        movement2D.MovementUpdate();
    }
}