using UnityEngine;
using System.Collections;

public class AndroidNeverSleep : MonoBehaviour {

    void Awake()
    {
#if UNITY_ANDROID
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif
    }
}
