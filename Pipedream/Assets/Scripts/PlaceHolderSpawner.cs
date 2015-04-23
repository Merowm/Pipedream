using UnityEngine;
using System.Collections;

public class PlaceHolderSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 scale = new Vector3(1,1,1);
    public float wallRotatingSpeed = 0.0f;

    private Transform placeHolderChild;
    private GameObject obj;

	void Awake ()
    {
        placeHolderChild = transform.GetChild(0).transform;
        obj = (GameObject)Instantiate(prefab, placeHolderChild.position, placeHolderChild.rotation);
        obj.transform.parent = transform.parent;

        foreach (Transform child in obj.transform)
        {
            child.transform.localScale = scale;
        }

        if (prefab.transform.name == "RotatingWall")
        {
            Debug.Log("asdadasdadsadasdasdasdas");
            if (wallRotatingSpeed != 0)
            {
                obj.transform.GetComponent<ObstacleRotation>().rotation = wallRotatingSpeed;
            }
        }

        Destroy(placeHolderChild.gameObject);
        Destroy(gameObject);
	}
}