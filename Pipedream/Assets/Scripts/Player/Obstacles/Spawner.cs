using UnityEngine;
using System.Collections;
//TODO:Rotation messed up, distancefromParent
public class Spawner : MonoBehaviour
{
    public GameObject theObject;
    public int amount;
    public Vector3 positionStart;
    public Vector3 positionGap;
    public Vector3 rotation;
    public Vector3 distanceFromParent;
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
            obj.transform.GetChild(0).transform.position += distanceFromParent;
            //Set rotation
            rotation = new Vector3(0,0,Random.Range(0,360));
            obj.transform.Rotate(rotation);
            //Set nextPosition
            nextPosition += positionGap;
            Debug.Log(rotation);
        }
	}
}
