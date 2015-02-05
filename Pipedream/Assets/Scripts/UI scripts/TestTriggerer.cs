using UnityEngine;
using System.Collections;

public class TestTriggerer : MonoBehaviour {

    public float a;

    Vector3 av;
	// Use this for initialization
	void Start () {
        av = new Vector3(0, 0, a);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += av * Time.deltaTime;
	}
}
