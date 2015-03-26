using UnityEngine;
using System.Collections;

public class ChangeLighting : MonoBehaviour
{
    public Color color;

    private Light[] lights;
    private Color lastColor; //Color last frame
    
    void Awake ()
    {
        lights = transform.GetComponentsInChildren<Light>();
        lastColor = color;
    }
    
    void Update ()
    {
        //Check if color needs to be updated
        if (color != lastColor)
        {
            //Update lastColor
            lastColor = color;
            //Change color for each light in lights
            for (int i = 0; i < lights.Length; i++)
            {
                lights [i].color = lastColor;
            }
        }
    }
}
