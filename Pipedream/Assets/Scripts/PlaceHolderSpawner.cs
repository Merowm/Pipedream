using UnityEngine;
using System.Collections;

public class PlaceHolderSpawner : MonoBehaviour
{
    public GameObject prefab;

    private Transform placeHolderChild;
    private GameObject theObject;

	void Awake ()
    {
        placeHolderChild = transform.GetChild(0).transform;
        theObject = (GameObject)Instantiate(prefab, placeHolderChild.position, placeHolderChild.rotation);
        theObject.transform.parent = transform.parent;
        Destroy(placeHolderChild.gameObject);
        Destroy(gameObject);
	}
}
