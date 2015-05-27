using UnityEngine;
using System.Collections;

public class Standby : MonoBehaviour {

    void Update()
    {
#if UNITY_EDITOR
    Application.LoadLevel("splash");
#elif UNITY_WEBPLAYER
    Application.LoadLevel("splash");
#elif UNITY_ANDROID
#else
    Application.LoadLevel("splash");
#endif
    }

}
