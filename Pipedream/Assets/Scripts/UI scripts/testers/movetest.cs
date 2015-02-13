using UnityEngine;
using System.Collections;

public class movetest : MonoBehaviour {

    Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = new Vector3(0, 0, 3);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += offset * Time.deltaTime;
        transform.Rotate(0, 0, 3 * Time.deltaTime);
	}
}
