using UnityEngine;
using System.Collections;

public class AndroidResolution : MonoBehaviour {

    const int targetW = 800;
    const int targetH = 600;

    void Awake()
    {
        //stretches view to maximum resolution without going more than screen width or height

#if UNITY_ANDROID
        Debug.Log("Native resolution: " + Screen.currentResolution.width + " X " + Screen.currentResolution.height);
        float widthpercentage = (float)Screen.currentResolution.width / (float)targetW;
        float heightpercentage = (float)Screen.currentResolution.height / (float)targetH;
        Debug.Log("Percentage: " + widthpercentage + " X " + heightpercentage);
        int newW, newH = 0;
        if (widthpercentage > heightpercentage)
        {
            newW = (int)(targetW * heightpercentage);
            newH = (int)(targetH * heightpercentage);
        }
        else
        {
            newW = (int)(targetW * widthpercentage);
            newH = (int)(targetH * widthpercentage);
        }
        Debug.Log("Setting screen resolution: " + newW + " X " + newH);
        Screen.SetResolution(newW, newH, true);
        Debug.Log("Current resolution: " + Screen.currentResolution.width + " X " + Screen.currentResolution.height);

#endif
    }

}
