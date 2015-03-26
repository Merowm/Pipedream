using UnityEngine;
using System.Collections;

public class movetest : MonoBehaviour {

    public float zOffset;
    Vector3 offset;
    //bool passed;
    
	// Use this for initialization
	void Start () {
        offset = new Vector3(0, 0, zOffset);
        
        //passed = false;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += offset * Time.deltaTime;
        //transform.Rotate(0, 0, 3 * Time.deltaTime);
        //if (!passed)
        //{
        //    if (transform.position.z > 50)
        //    {
        //        Debug.Log("one more");
        //        passed = true;
        //        Destroy(this.gameObject);
        //    }
        //}
        
	}
}
