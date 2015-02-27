using UnityEngine;
using System.Collections;

public class audioTest : MonoBehaviour {

    public Transform source;
    public float spawndistance;
    public int amount;
    float interval;
    Vector3 sp;
    int i;
	// Use this for initialization
	void Start () {
        sp = new Vector3(0, 0, -50);
        i = 0;
        //for (int i = 0; i < amount; i++)
        //{
        //    if (Instantiate(source, sp, Quaternion.identity) != null)
        //        Debug.Log(i);
        //    sp.z -= spawndistance;
        //}
	}
	
	// Update is called once per frame
	void Update () {
        interval += Time.deltaTime;
        if (interval > spawndistance)
        {
            Instantiate(source, sp, Quaternion.identity);
            interval = 0;
            Debug.Log(i++);
        }
	}
}
