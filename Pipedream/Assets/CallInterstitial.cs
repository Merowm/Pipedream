using UnityEngine;
using System.Collections;

public class CallInterstitial : MonoBehaviour {
    
    void Awake()
    {
#if UNITY_ANDROID
        Advertisement ad;
        if (ad = FindObjectOfType<Advertisement>())
        {
            ad.ShowInterstitial();
        }
        else
        {
            Destroy(gameObject);
        }
#else
        Destroy(gameObject);
#endif
    }

}
