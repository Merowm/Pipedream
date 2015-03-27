using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfiniteLevel : MonoBehaviour {

    //LOGIC
    //2 parts active at any time
    //parts are randomized
    //when player reaches end of first part, 
    //despawn first part,
    //shift everything else one tube length back (back to origin)
    //spawn new part behind second part
    //repeat

    //list of parts available to spawn
    public List<GameObject> availablePartsList = new List<GameObject>();

    //list of active parts in the world (for easy management)
    public List<GameObject> activePartsList = new List<GameObject>();

    //length of each part
    public int LengthOfPart = 1000;

    //player object
    public GameObject player;

    //offset of starting point
    public Vector3 startingOffset;

    void Awake(){
        //spawn initial 2 tubes 
        SpawnPart(Random.Range(0, availablePartsList.Count), new Vector3(startingOffset.x, startingOffset.y, 0));
        SpawnPart(Random.Range(0, availablePartsList.Count), new Vector3(0, 0, LengthOfPart));
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
        //instantiate specified part at specified position
        GameObject newPart = Instantiate(availablePartsList [indexToSpawn], position, Quaternion.identity) as GameObject;
        //add new part to list for easy management
        activePartsList.Add(newPart);

        newPart.transform.FindChild("HyperPart_x10").gameObject.SetActive(true);
    }

    //when the player reaches the end of the first tube
    private void OnFirstPartEnd(){

        //destroy first tube
        Destroy(activePartsList [0]);
        //remove from list
        activePartsList.RemoveAt(0);

        //shift second tube and player back to origin
        activePartsList [0].transform.position -= new Vector3(0, 0, LengthOfPart);
        player.transform.position -= new Vector3(0, 0, LengthOfPart);

        //spawn new tube
        SpawnPart(Random.Range(0, availablePartsList.Count), new Vector3(startingOffset.x, startingOffset.y, LengthOfPart));
    }

}
