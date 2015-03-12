using UnityEngine;
using System.Collections;

public class BlinkingLight : MonoBehaviour
{
    public bool blinking = true;
    public float lightRangeMax = 3.0f;
    public float lightRangeMin = 0.5f;
    public float waitingTime = 0.5f;

    void Update()
    {
        if (blinking)
        {
            //Invoke("Blink", 2);
        }
    }

    void Blink()
    {
        if (light.range == lightRangeMax)
        {
            light.range = lightRangeMin;
        }
        else light.range = lightRangeMax;
    }
}
