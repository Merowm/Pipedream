using UnityEngine;
using System.Collections;

public class CustomizationSpawner : MonoBehaviour
{
    public GameObject theObject;
    public int amount = 100;
    public float positionGap = 10.0f;
    public float distanceFromParent = -4.5f;
    public float angleMax = 335.0f;
    public float angleMin = 25.0f;

    private HyperTunnelMovement movement;
    private Vector3 nextPosition;
    private float rotation;

    void Awake ()
    {
        movement = transform.GetComponent<HyperTunnelMovement>();
    }

    void Start ()
    {
        SpawnObjects();
    }

    public void MoveObjects()
    {
        foreach(Transform child in transform)
        {
            float z = child.localPosition.z - movement.distanceReset;
            child.localPosition = new Vector3(0,0,z);
        }
    }

    public void SpawnObjects()
    {
        nextPosition = transform.position;
        
        for (int i = 0; i < amount; i++)
        {
            //Spawn theObject
            GameObject obj;
            obj = (GameObject)Instantiate(theObject);
            
            //Tag object as SpawnedObject
            obj.tag = "SpawnedObject";
            
            //Get object's child's transform
            Transform childObjectTransform = obj.transform.GetChild(0).transform;
            
            //Set positions
            obj.transform.position = nextPosition;
            childObjectTransform.position += new Vector3(0,distanceFromParent,0);
            
            //Set rotation
            rotation = Random.Range(angleMin,angleMax + 0.1f);
            obj.transform.Rotate(0,0,rotation);
            
            //Set nextPosition
            nextPosition += new Vector3(0,0,positionGap);
            
            //Set child's position as parent's position
            //obj.transform.position = childObjectTransform.position;
            //childObjectTransform.position = obj.transform.position;

            //Set spawner object as parent
            obj.transform.parent = transform;
        }
    }

    public void ClearObjects()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
