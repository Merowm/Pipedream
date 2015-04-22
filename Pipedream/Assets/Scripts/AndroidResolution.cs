using UnityEngine;
using System.Collections;

public class AndroidResolution : MonoBehaviour {

    void Awake()
    {
#if UNITY_ANDROID
        Screen.SetResolution(800, 600, true);
#endif
    }

}
