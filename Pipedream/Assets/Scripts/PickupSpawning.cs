using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickupSpawning : MonoBehaviour
{
    public List<GameObject> pickups = new List<GameObject>();
    public float lengthTravelled = 0.0f;

    private InfiniteLevel infinite;
    private GameObject obj;
    private GameObject pickupMover;

	void Awake ()
    {
        infinite = transform.GetComponent<InfiniteLevel>();
        lengthTravelled = -infinite.lengthOfPart;
        pickupMover = transform.GetChild(0).gameObject;
        //Spawn the first pickup
        SpawnPickup(new Vector3(0.0f,0.0f,1000.0f));
	}

	void Update ()
    {
	    if (lengthTravelled == 1000.0f)
        {
            foreach(Transform child in transform.GetChild(0))
            {
                Destroy(child.gameObject);
            }
            SpawnPickup(new Vector3(0.0f,0.0f,800.0f));
            lengthTravelled = 0.0f;
        }
	}

    public void MovePickups()
    {
        foreach(Transform child in transform.GetChild(0))
        {
            child.localPosition -= new Vector3(0, 0, infinite.lengthOfPart);
        }
    }

    private void SpawnPickup(Vector3 position)
    {
        //Spawn a random pickup
        int index;
        index = Random.Range(0,100);
        Debug.Log("Spawning index " + index);
        //Pickup to be spawned
        //Repair with a 50% chance
        if (index < 50)
        {
            index = 0;
        }
        //Invulnerability with a 20% chance
        else if (index >= 50 && index < 70)
        {
            index = 1;
        }
        //Time slow with a 30% chance
        else if (index >= 70)
        {
            index = 2;
        }
        //Spawn the object
        obj = (GameObject)Instantiate(pickups[index]);
        //Set it's parent
        obj.transform.parent = pickupMover.transform;
        //Set position
        obj.transform.position = position;
        //Set rotation
        Vector3 rotation = new Vector3(0,0,0);//Random.Range(0.0f,360.0f));
        obj.transform.Rotate(rotation);
        //Reset rotation if needed in script "SpaenCollision"
    }
}
