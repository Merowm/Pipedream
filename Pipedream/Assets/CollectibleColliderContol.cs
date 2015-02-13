using UnityEngine;
using System.Collections;

public class CollectibleColliderContol : MonoBehaviour
{
    private SphereCollider sphere;
    private BoxCollider box;

	void Awake ()
    {
        sphere = transform.parent.GetComponent<SphereCollider>();
        box = transform.GetComponent<BoxCollider>();
	}

	void Update ()
    {
	    if (MovementForward.inHyperSpace)
        {
            sphere.enabled = false;
            box.enabled = true;
        }
        else
        {
            sphere.enabled = true;
            box.enabled = false;
        }
	}
}
