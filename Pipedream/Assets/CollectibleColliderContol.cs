using UnityEngine;
using System.Collections;

public class CollectibleColliderContol : MonoBehaviour
{
    private BoxCollider hyperspaceBox;
    private BoxCollider spaceBox;

	void Awake ()
    {
        hyperspaceBox = transform.FindChild("HyperspaceCollider").GetComponent<BoxCollider>();
        spaceBox = transform.FindChild("SpaceCollider").GetComponent<BoxCollider>();
	}

	void Update ()
    {
	    if (MovementForward.inHyperSpace)
        {
            hyperspaceBox.enabled = true;
            spaceBox.enabled = false;
        }
        else
        {
            hyperspaceBox.enabled = false;
            spaceBox.enabled = true;
        }
	}
}
