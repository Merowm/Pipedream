using UnityEngine;
using System.Collections.Generic;

public class SpaceDriveState : MonoBehaviour
{
    public int currentDriveIndex = 0;
    public List<GameObject> driveStatePositionObjects; //List of level portion objects
    public List<Vector3> driveStatePositions; //List of where the level portions start at

    private Movement2D movement2D;
    private Transform shipTransform;
    private GameObject hyperspaceHorizont;
    private GameObject boundaryCircle;
    private Vector3 distanceToParentAtStart; //The position the player should be at when a level portion starts

	void Awake ()
    {
        //Finds all the portions of the level
        GameObject[] driveStates = GameObject.FindGameObjectsWithTag("DriveState");

        //Converts the level portion array to a list
        for (int i = driveStates.Length; i > 0; i--)
        {
            driveStatePositionObjects.Add(driveStates[i - 1]);
        }

        //Sorts the level portions into numerical order from 0 onwards
        driveStatePositionObjects.Sort(SortByName);

        //Lists the starting positions for each portion of the level
        for (int i = 0; i < driveStatePositionObjects.Count; i++)
        {
            driveStatePositions.Add(driveStatePositionObjects[i].transform.position);
        }

        movement2D = transform.GetComponent<Movement2D>();
        shipTransform = transform.FindChild("Ship").transform;
        hyperspaceHorizont = transform.FindChild("HyperspaceHorizont").gameObject;
        boundaryCircle = transform.FindChild("BoundaryCircle").gameObject;
        distanceToParentAtStart = new Vector3(0, Vector3.Distance(shipTransform.position, transform.position), 0);
	}

    //Cycles forward on the level portions list,
    //if at end reverts to beginning of the list
    public void SetDriveStateForward()
    {
        if (currentDriveIndex < driveStatePositions.Count - 1)
        {
            currentDriveIndex++;
        }
        else currentDriveIndex = 0;
    }

    //Cycles backward on the level portions list,
    //if at beginning reverts to the end of the list
    public void SetDriveStateBackward()
    {
        if (currentDriveIndex > 0)
        {
            currentDriveIndex--;
        }
        else currentDriveIndex = driveStatePositions.Count - 1;
    }

    public void DisengagingHyperSpace()
    {
        MovementForward.inHyperspace = false;
        SetDriveVariables();
    }
    
    public void EngagingHyperSpace()
    {
        MovementForward.inHyperspace = true;
        hyperspaceHorizont.SetActive(true);
        SetDriveVariables();
    }

    //Sets player's position, rotation, and resets speeds
    void SetDriveVariables()
    {
        transform.rotation = new Quaternion(0,0,0,0);
        transform.position = driveStatePositions[currentDriveIndex];
        shipTransform.position = transform.position - distanceToParentAtStart;
        movement2D.ResetVariables();
    }

    int SortByName(GameObject object1, GameObject object2)
    {
        return object1.name.CompareTo(object2.name);
    }
}


























