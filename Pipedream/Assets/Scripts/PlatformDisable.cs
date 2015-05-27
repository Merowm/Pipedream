using UnityEngine;
using System.Collections;

public class PlatformDisable : MonoBehaviour {

    public RuntimePlatform[] platformWhitelist;

    void Awake()
    {
        foreach (RuntimePlatform platform in platformWhitelist)
        {
            if (Application.platform == platform)
            {
                return;
            }
        }
        Destroy(gameObject);
    }

}
