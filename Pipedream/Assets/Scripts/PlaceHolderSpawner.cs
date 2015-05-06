using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PlaceHolderSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 scale = new Vector3(0,0,0);
    public bool movingAsteroid = false;
    public float wallRotatingSpeed = 0.0f;
    public float lotusRotatingSpeed = 0.0f;

    public List<Material> materials;

    private Transform placeHolderChild;
    private GameObject obj;

	void Awake ()
    {
        if (Application.isPlaying)
        {
            placeHolderChild = transform.GetChild(0).transform;

            obj = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
            obj.transform.parent = transform.parent;

            if (prefab.transform.name == "lotus")
            {
                placeHolderChild.localPosition = new Vector3(0,-5.5f,0);
            }
            else if (prefab.transform.name == "HyperAsteroid01")
            {
                placeHolderChild.localPosition = new Vector3(0,-5.0f,0);
            }

            if (prefab.transform.name != "RotatingWall")
            {
                obj.transform.GetChild(0).transform.position = placeHolderChild.position;
            }

            if (prefab.transform.name == "lotus" && lotusRotatingSpeed != 0)
            {
                //obj = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
                //obj.transform.parent = transform.parent;
                //obj.transform.GetChild(0).transform.localPosition = new Vector3(0,-4,0);

                obj.transform.GetComponent<ObstacleRotation>().rotation = lotusRotatingSpeed;
                Debug.Log("...");
            }
            else
            {
                //obj = (GameObject)Instantiate(prefab, placeHolderChild.position, placeHolderChild.rotation);
                //obj.transform.parent = transform.parent;
            }

            if (scale != new Vector3(0,0,0))
            {
                foreach (Transform child in obj.transform)
                {
                    child.transform.localScale = scale;
                }
            }

            if (prefab.transform.name == "HyperAsteroid01")
            {
                if (movingAsteroid)
                {
                    obj.GetComponent<ObstacleUpDown>().enabled = true;
                }
            }
            else if (prefab.transform.name == "RotatingWall")
            {
                if (wallRotatingSpeed != 0)
                {
                    obj.transform.GetComponent<ObstacleRotation>().rotation = wallRotatingSpeed;
                }
            }

            Destroy(placeHolderChild.gameObject);
            Destroy(gameObject);
        }
	}

    //Color changing of the place holder object happens here
#if UNITY_EDITOR
    void Update ()
    {
        if(UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
        {
            this.enabled = false;
        }
        else
        {
            //
            if (prefab.transform.name == "HyperAsteroid01")
            {
                if (movingAsteroid)
                {
                    //transform.GetComponentInChildren<MeshRenderer>().material;
                }
            }
        }
    }
#endif
}