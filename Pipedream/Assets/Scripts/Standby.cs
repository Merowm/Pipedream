using UnityEngine;
using System.Collections;

public class Standby : MonoBehaviour {

    void Update()
    {
#if UNITY_EDITOR
    Application.LoadLevel("StartMenu");
#elif UNITY_WEBPLAYER
    Application.LoadLevel("StartMenu");
#elif UNITY_ANDROID
#else
    Application.LoadLevel("StartMenu");
#endif
    }

}
