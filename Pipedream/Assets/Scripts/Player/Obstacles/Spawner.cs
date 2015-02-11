using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject theObject;
    public int amount;
    public Vector3 positionStart;
    public Vector3 positionGap;

    private Vector3 nextPosition;

	void Awake ()
    {
        nextPosition = positionStart;

        for (int i = 0; i < amount; i++)
        {
            //Spawn theObject
            Instantiate(theObject);
            //Set Position
            theObject.transform.position = nextPosition;
            //Rotation
            Quaternion rotation;
            rotation = new Quaternion(0,0,Random.Range(0,360),0);
            theObject.transform.rotation = rotation;
            //Set nextPosition
            nextPosition = new Vector3(positionGap.x + nextPosition.x,
                                       positionGap.y + nextPosition.y,
                                       positionGap.z + nextPosition.z);
        }
	}
}
