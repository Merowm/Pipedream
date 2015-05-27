using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeSkybox : MonoBehaviour
{
    public List<Material> skyboxes = new List<Material>();

    private bool wasInHyperspace = false; //What MovementForward.inHyperspace was last frame

	void Awake ()
    {
	    
	}

	void Update ()
    {
	    if (MovementForward.inHyperspace)
        {
            if (!wasInHyperspace)
            {
                RenderSettings.skybox = skyboxes[Random.Range(2, skyboxes.Count)];
            }
        }
        else
        {
            if (wasInHyperspace)
            {
                RenderSettings.skybox = skyboxes[1];
            }
        }
        wasInHyperspace = MovementForward.inHyperspace;
	}
}
