using UnityEngine;
using System.Collections;

public class CountScore : MonoBehaviour {

	int points;
	// Use this for initialization
	void Start () {
		guiText.text = points.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void AddScore(int newPoints)
	{
		points += newPoints;
		guiText.text = points.ToString ();
	}
}
