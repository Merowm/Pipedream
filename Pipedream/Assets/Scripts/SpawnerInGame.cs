using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class SpawnerInGame : MonoBehaviour
{
    public GameObject theObject;
    public Transform transfrom;
    public int amount;
    public float positionGap;
    public float nextObjectAngleRange;
    public float distanceFromParent;
    public bool randomizeDistanceFromParent = false;
    public float distanceFromParentMax = 10.0f;
    public bool noGraphicsRotation = false;

    private Vector3 nextPosition;
    private float rotation;
    private float lastObjectRotation;

	void Start ()
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
            if (randomizeDistanceFromParent)
            {
                distanceFromParent = Random.Range(0.0f,distanceFromParentMax + 0.1f);
            }
            childObjectTransform.position += new Vector3(0,distanceFromParent,0);

            //Set rotation
            if (nextObjectAngleRange > 0)
            {
                float angleMax = lastObjectRotation + nextObjectAngleRange;
                float angleMin = lastObjectRotation - nextObjectAngleRange;
                rotation = Random.Range(angleMin,angleMax + 0.1f);
            }
            else rotation = Random.Range(0.0f,360.1f);

            obj.transform.Rotate(0,0,rotation);
            if (noGraphicsRotation)
            {
                childObjectTransform.FindChild("Graphics").transform.Rotate(0,0,-rotation);
                childObjectTransform.FindChild("SpaceCollider").transform.Rotate(0,0,-rotation);
            }

            lastObjectRotation = rotation;

            //Set nextPosition
            nextPosition += new Vector3(0,0,positionGap);

            //Set child's position as parent's position
            obj.transform.position = childObjectTransform.position;
            childObjectTransform.position = obj.transform.position;
        }
	}
}
