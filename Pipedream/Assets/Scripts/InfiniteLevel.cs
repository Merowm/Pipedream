using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfiniteLevel : MonoBehaviour {

    //LOGIC
    //spawn all the different parts, deactivate, and add them to the object pool
    //2 parts active at any time
    //parts are randomized
    //when player reaches end of first part, 
    //deactivate first part,
    //shift everything else one tube length back (back to origin)
    //activate new part behind second part
    //repeat
    //1 2
    //  2
    //2
    //2 3

    //list of parts available to spawn
    public List<GameObject> availablePartsList = new List<GameObject>();

    //list of active parts in the world (for easy management)
    private List<GameObject> activePartsList = new List<GameObject>();

    //object pool
    private List<GameObject> poolList = new List<GameObject>();

    //length of each part
    public int LengthOfPart = 1000;

    //player object
    public GameObject player;

    //offset of starting point
    public Vector3 startingOffset;

    void Awake(){
        //spawn objects for pooling
        foreach (GameObject go in availablePartsList)
        {
            GameObject go1 = Instantiate(go);
            go1.transform.FindChild("HyperParts").gameObject.SetActive(true);
            go1.SetActive(false);
            poolList.Add(go1);
        }

        //spawn initial 2 tubes 
        SpawnPart(Random.Range(0, poolList.Count), new Vector3(startingOffset.x, startingOffset.y, 0));
        SpawnPart(Random.Range(0, poolList.Count), new Vector3(startingOffset.x, startingOffset.y, LengthOfPart));
    }
	
	// Update is called once per frame
	void Update () {
	    //check if player has reached end of first tube
        if (player.transform.position.z >= LengthOfPart)
        {
            OnFirstPartEnd();
        }
	}

    //spawns specified part at specified position
    private void SpawnPart(int indexToSpawn, Vector3 position){
        //activate specified part at specified position
        GameObject go = poolList [indexToSpawn];
        SetActiveRecursively(go, true);
        go.transform.position = position;
        //remove part from pool
        poolList.Remove(go);
        //add new part to active list for easy management
        activePartsList.Add(go);

    }

    //when the player reaches the end of the first tube
    private void OnFirstPartEnd(){

        //deactivate first tube
        GameObject go = activePartsList [0];
        go.SetActive(false);
        //remove from list
        activePartsList.Remove(go);
        //add back to pool
        poolList.Add(go);
        //shift second tube and player back to origin
        activePartsList [0].transform.position -= new Vector3(0, 0, LengthOfPart);
        player.transform.position -= new Vector3(0, 0, LengthOfPart);

        //spawn new tube
        SpawnPart(Random.Range(0, poolList.Count), new Vector3(startingOffset.x, startingOffset.y, LengthOfPart));
    }

    public static void SetActiveRecursively(GameObject root, bool active){ 
        root.SetActive(active);
        foreach (Transform childTransform in root.transform)
        {
            SetActiveRecursively(childTransform.gameObject, active);
        }
    }



}
