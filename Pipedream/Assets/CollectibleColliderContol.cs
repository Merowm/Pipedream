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
            /*
            float x = transform.parent.rotation.x;// - transform.parent.parent.rotation.x;
            float y = transform.parent.rotation.y;// - transform.parent.parent.rotation.y;
            float z = - transform.parent.parent.rotation.z;
            float w = transform.parent.rotation.w;// - transform.parent.parent.rotation.w;

            transform.parent.rotation = new Quaternion(x,y,z,w);*/
        }
	}
}
