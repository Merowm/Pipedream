using UnityEngine;
using System.Collections.Generic;

public class SpaceDriveState : MonoBehaviour
{
    public int currentDriveIndex = 0;
    public List<GameObject> driveStatePositionObjects;
    public List<Vector3> driveStatePositions;

    private Movement2D movement2D;
    private Transform shipTransform;
    private GameObject hyperspaceHorizont;
    private GameObject boundaryCircle;
    private Vector3 distanceToParentAtStart;

	void Awake ()
    {
        GameObject[] driveStates = GameObject.FindGameObjectsWithTag("DriveState");

        for (int i = driveStates.Length; i > 0; i--)
        {
            driveStatePositionObjects.Add(driveStates[i - 1]);
        }

        driveStatePositionObjects.Sort(SortByName);

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

    public void SetDriveStateForward()
    {
        if (currentDriveIndex < driveStatePositions.Count - 1)
        {
            currentDriveIndex++;
        }
        else currentDriveIndex = 0;
    }
    
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
        MovementForward.inHyperSpace = false;
        SetDriveVariables();
    }
    
    public void EngagingHyperSpace()
    {
        MovementForward.inHyperSpace = true;
        hyperspaceHorizont.SetActive(true);
        SetDriveVariables();
    }

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


























