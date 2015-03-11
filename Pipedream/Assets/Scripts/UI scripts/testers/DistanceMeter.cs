using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DistanceMeter : MonoBehaviour {

    GameObject[] entrances;
    GameObject[] exits;
    float fullrun;
    float donerun;
	// Use this for initialization
	void Start ()
    {
        entrances = GameObject.FindGameObjectsWithTag("EntranceGate");
        exits = GameObject.FindGameObjectsWithTag("ExitGate");
        foreach (GameObject e in entrances)
        {
            fullrun += e.transform.position.z;
        }
        foreach (GameObject e in exits)
        {
            fullrun += e.transform.position.z;
        }
        for (int i = 0; i < exits.Length; ++i)
        {

        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
