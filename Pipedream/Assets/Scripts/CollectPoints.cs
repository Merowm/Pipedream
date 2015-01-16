using UnityEngine;
using System.Collections;

public class CollectPoints : MonoBehaviour {
	GameObject score;
	// Use this for initialization
	void Start () {
		score = GameObject.FindGameObjectWithTag ("Scoretext");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other)
	{
		score.GetComponent<CountScore>().AddScore(10);
		Destroy (this.gameObject);
	}
}
