using UnityEngine;
using System.Collections;

public class audioTest : MonoBehaviour {

    public Transform source;
    public float spawndistance;
    public int amount;
    
    Vector3 sp;
	// Use this for initialization
	void Start () {
        sp = new Vector3(0, 0, -50);
	    for (int i = 0; i < amount; i++)
        {
            if (Instantiate(source, sp, Quaternion.identity) != null)
                Debug.Log(i);
            sp.z -= spawndistance;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
