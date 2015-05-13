using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    public int lengthOfPart = 1000;

    //player object
    public GameObject player;

    //offset of starting point
    public Vector3 startingOffset;

    private const int InitialSpawnNumber = 5;

    //parent containing parts
    public Transform partsParent;

    //accumulative score stuff
    public int scoreGain = 20;
    public float scoreAddingInterval = 1.0f;
    private float timer = 0.0f;

    private LevelTimer levelTimer;
    private GameObject cameraParent;

    //For pickup spawning
    public PickupSpawning spawning;

    void Awake(){
        //list available parts
        UpdateAvailableParts();

        //spawn objects for pooling
        foreach (GameObject go in availablePartsList)
        {
            GameObject go1 = Instantiate(go);
            go1.transform.parent = partsParent;
            go1.transform.position = new Vector3(startingOffset.x, startingOffset.y, startingOffset.z + lengthOfPart * InitialSpawnNumber);
            //go1.transform.FindChild("HyperParts").gameObject.SetActive(true);
            go1.SetActive(false);
            poolList.Add(go1);
        }

        //spawn initial half of available tubes
        for (int i = 0; i < InitialSpawnNumber; ++i)
        {
            SpawnPart(Random.Range(0, poolList.Count), new Vector3(startingOffset.x, startingOffset.y, startingOffset.z + activePartsList.Count * lengthOfPart));
        }
        //empty out first tube so player doesn't collide when just spawning in
        activePartsList [0].transform.FindChild("Obstacles").gameObject.SetActive(false);
        activePartsList [1].transform.FindChild("Obstacles").gameObject.SetActive(false);
        activePartsList [0].transform.FindChild("Collectables").gameObject.SetActive(false);
        activePartsList [1].transform.FindChild("Collectables").gameObject.SetActive(false);
        //activePartsList [2].transform.FindChild("Obstacles").gameObject.SetActive(false);

        if (GameObject.FindWithTag("levelTimer") != null)
        {
            levelTimer = GameObject.FindGameObjectWithTag("levelTimer").GetComponent<LevelTimer>();
        }
        if (GameObject.FindWithTag("CameraParent") != null)
        {
            cameraParent = GameObject.FindGameObjectWithTag("CameraParent");
        }
        spawning = transform.GetComponent<PickupSpawning>();
    }
	
	// Update is called once per frame
	void Update () {
	    //check if player has reached end of first tube
        if (player.transform.position.z -startingOffset.z >= lengthOfPart)
        {
            OnFirstPartEnd();
            spawning.MovePickups();
            spawning.lengthTravelled += lengthOfPart;
        }
        ScoreOverTime();
	}

    private void ScoreOverTime ()
    {
        //Update timer
        timer += Time.deltaTime;
        //When timer finishes
        if (timer >= scoreAddingInterval)
        {
            //Add score
            if (MovementForward.inHyperspace)
            {
                levelTimer.AddScore((int)(scoreGain * MovementForward.difficultyMultiplier));
            }
            //Reset timer
            timer = 0.0f;
        }
    }

    //spawns specified part at specified position
    private void SpawnPart(int indexToSpawn, Vector3 position){
        //activate specified part at specified position
        GameObject go = poolList [indexToSpawn];
        go.transform.position = position;
        go.SetActive(true);
        go.transform.FindChild("Obstacles").gameObject.SetActive(true);
        foreach (Transform child in go.transform.FindChild("Obstacles").transform)
        {
            child.gameObject.SetActive(true);
        }
        go.transform.FindChild("Collectables").gameObject.SetActive(true);
        foreach (Transform child in go.transform.FindChild("Collectables").transform)
        {
            child.gameObject.SetActive(true);
        }
        //remove part from pool
        poolList.Remove(go);
        //add new part to active list for easy management
        activePartsList.Add(go);
        Debug.Log("Spawned " + go);

    }

    //when the player reaches the end of the first tube
    private void OnFirstPartEnd(){

        //deactivate first tube
        GameObject go = activePartsList[0];
        go.SetActive(false);
        go.transform.Translate(0, 0, activePartsList.Count * lengthOfPart);
        //remove from list
        activePartsList.Remove(go);
        //add back to pool
        poolList.Add(go);
        //shift second tube and player back to origin
        //Camera.main.transform.position -= new Vector3(0, 0, LengthOfPart);
        cameraParent.transform.position -= new Vector3(0, 0, lengthOfPart);
        player.transform.position -= new Vector3(0, 0, lengthOfPart);

        for (int i = 0; i < activePartsList.Count; ++i)
        {
            activePartsList[i].transform.position -= new Vector3(0, 0, lengthOfPart);
        }
        //spawn new tube
        SpawnPart(Random.Range(0, poolList.Count), new Vector3(startingOffset.x, startingOffset.y, startingOffset.z + activePartsList.Count * lengthOfPart));
    }

    public static void SetActiveRecursively(GameObject root, bool active){ 
        root.SetActive(active);
        foreach (Transform childTransform in root.transform)
        {
            SetActiveRecursively(childTransform.gameObject, active);
        }
    }

    public void Restart(){
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void UpdateAvailableParts()
    {
        availablePartsList.Clear();

        Object[] objects = Resources.LoadAll("");
        /*.Add((GameObject)objects[0]);
        availablePartsList.Add((GameObject)objects[1]);
        availablePartsList.Add((GameObject)objects[2]);
        availablePartsList.Add((GameObject)objects[3]);
        availablePartsList.Add((GameObject)objects[4]);*/
        for (int i = 0; i < objects.Length; i++)
        {
            availablePartsList.Add((GameObject)objects[i]);
        }
    }
}
