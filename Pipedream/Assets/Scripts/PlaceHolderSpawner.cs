using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[ExecuteInEditMode]
public class PlaceHolderSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 scale = new Vector3(0,0,0);
    public bool movingObject = false;
    public bool negativeRotation = false;
    public float wallRotatingSpeed = 0.0f;
    public float lotusRotatingSpeed = 0.0f;

    private Transform placeHolderChild;
    private GameObject obj;

	void Awake ()
    {
        placeHolderChild = transform.GetChild(0).transform;

        obj = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
        obj.transform.parent = transform.parent;

        if (prefab.transform.name == "lotus" || prefab.transform.name == "goldenLotus")
        {
            placeHolderChild.localPosition = new Vector3(0,-5.5f,0);
        }
        else if (prefab.transform.name == "HyperAsteroid01")
        {
            placeHolderChild.localPosition = new Vector3(0,-5.0f,0);
        }

        if (prefab.transform.name != "RotatingWall" && prefab.transform.name != "TriWall")
        {
            obj.transform.GetChild(0).transform.position = placeHolderChild.position;
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
            if (movingObject)
            {
                obj.GetComponent<ObstacleUpDown>().enabled = true;
            }
        }
        else if (prefab.transform.name == "lotus" || prefab.transform.name == "goldenLotus")
        {
            if (movingObject)
            {
                obj.GetComponent<ObjectRotation>().enabled = true;

                if (lotusRotatingSpeed != 0)
                {
                    obj.transform.GetComponent<ObjectRotation>().rotationSpeed = lotusRotatingSpeed;
                }
            }
        }
        else if (prefab.transform.name == "RotatingWall" || prefab.transform.name == "TriWall")
        {
            if (wallRotatingSpeed != 0)
            {
                obj.transform.GetComponent<ObjectRotation>().rotationSpeed = wallRotatingSpeed;
            }
        }

        if (negativeRotation)
        {
            wallRotatingSpeed = -wallRotatingSpeed;
            lotusRotatingSpeed = -lotusRotatingSpeed;
            if (prefab.transform.name == "lotus" ||
                prefab.transform.name == "goldenLotus" ||
                prefab.transform.name == "RotatingWall" ||
                prefab.transform.name == "TriWall")
            {
                obj.transform.GetComponent<ObjectRotation>().rotationSpeed *= -1.0f;
            }
        }

        Destroy(placeHolderChild.gameObject);
        Destroy(gameObject);
    }
}

    //Color changing of the place holder object happens here
/*#if UNITY_EDITOR
    void Update ()
    {
        if(UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
        {
            this.enabled = false;
        }
        else
        {
            if (prefab.transform.name == "HyperAsteroid01")
            {
                if (!movingAsteroid)
                {
                    transform.GetComponentInChildren<MeshRenderer>().material = materials[0];
                }
                else
                {
                    transform.GetComponentInChildren<MeshRenderer>().material = materials[1];
                }
            }
        }
    }
#endif*/
//}