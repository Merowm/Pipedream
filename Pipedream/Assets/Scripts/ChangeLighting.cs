using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ChangeLighting : MonoBehaviour
{
    public Color color;

    private Light[] lights;
    private Color lastColor; //Color last frame
    
    void Awake ()
    {
        lights = transform.GetComponentsInChildren<Light>();
        lastColor = color;

        Light transformLight = transform.GetComponent<Light>();

        for (int i = 0; i < lights.Length; i++)
        {
            //lights[i].cullingMask = transformLight.cullingMask;
        }
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
