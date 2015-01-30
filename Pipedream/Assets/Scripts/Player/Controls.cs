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

        if (Input.GetAxis("Horizontal") == 1)
        {
            controls["Right"] = true;
            controls["Left"] = false;
        }
        else if (Input.GetAxis("Horizontal") == -1)
        {
            controls["Left"] = true;
            controls["Right"] = false;
        }
        else
        {
            controls["Right"] = false;
            controls["Left"] = false;
        }

        if (Input.GetAxis("Vertical") == 1)
        {
            controls["Up"] = true;
            controls["Down"] = false;
        }
        else if (Input.GetAxis("Vertical") == -1)
        {
            controls["Down"] = true;
            controls["Up"] = false;
        }
        else
        {
            controls["Up"] = false;
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
