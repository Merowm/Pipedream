using UnityEngine;
using System.Collections;

public class ChangeLighting : MonoBehaviour
{
    public Color color;

    private Transform tunnelPart1;
    private Transform tunnelPart2;
    private Light[] lights;
    
    void Awake ()
    {
        tunnelPart1 = transform.GetChild(0);
        tunnelPart2 = transform.GetChild(1);
        lights = transform.GetComponentsInChildren<Light>();

        for (int i = 0; i < lights.Length; i++)
        {
            //lights[i].color = color;
        }
    }
    
    void Update ()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].color = color;
        }
    }
}
