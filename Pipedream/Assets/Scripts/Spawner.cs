using UnityEngine;
using System.Collections;
//TODO:Rotation messed up, distancefromParent
public class Spawner : MonoBehaviour
{
    public GameObject theObject;
    public int amount;
    public Vector3 positionStart;
    public float positionGap;
    public float rotation;
    public float distanceFromParent;
    public bool randomizeDistanceFromParent = false;

    private Vector3 nextPosition;

	void Start ()
    {
        nextPosition = positionStart;

        for (int i = 0; i < amount; i++)
        {
            //Spawn theObject
            GameObject obj;
            obj = (GameObject)Instantiate(theObject);
            //Set positions
            obj.transform.position = nextPosition;
            if (randomizeDistanceFromParent)
            {
                distanceFromParent = Random.Range(0.0f,10.1f);
            }
            obj.transform.GetChild(0).transform.position += new Vector3(0,distanceFromParent,0);
            //Set rotation
            rotation = Random.Range(0,361);
            obj.transform.Rotate(0,0,rotation);
            //Set nextPosition
            nextPosition += new Vector3(0,0,positionGap);
            Debug.Log(rotation);
        }
	}
}
