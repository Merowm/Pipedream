using UnityEngine;
using System.Collections.Generic;

public class Controls : MonoBehaviour
{
    public enum CONTROL_SCHEME {Keyboard, Mouse}
    public int index = 0;
    public Dictionary<string, bool> controls;

    public bool right;
    public bool left;
    public bool up;
    public bool down;

    private Movement2D movement;

	void Awake ()
    {
        movement = transform.GetComponent<Movement2D>();

        controls = new Dictionary<string, bool>();
        controls.Add("Right", false);
        controls.Add("Left", false);
        controls.Add("Up", false);
        controls.Add("Down", false);
	}

    void Update ()
    {
        //Switches between control schemes, default scheme is keyboard
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (index == 0)
            {
                index++;
            }
            else if (index == 1)
            {
                index--;
            }
        }

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

        right = controls["Right"];
        left = controls["Left"];
        up = controls["Up"];
        down = controls["Down"];
    }
	
	void FixedUpdate ()
    {
        //Switches between control schemes, default scheme is keyboard
        if (index == 0)
        {
            ChooseContolsScheme(CONTROL_SCHEME.Keyboard);
        }
        else if (index == 1)
        {
            ChooseContolsScheme(CONTROL_SCHEME.Mouse);
        }
    }

    void ChooseContolsScheme(CONTROL_SCHEME scheme)
    {
        switch(scheme)
        {
            case CONTROL_SCHEME.Keyboard:
                movement.KeyboardControls();
                break;
                
            case CONTROL_SCHEME.Mouse:
                movement.MouseControls();
                break;
        }
    }
}
