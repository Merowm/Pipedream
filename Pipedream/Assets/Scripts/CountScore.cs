using UnityEngine;
using System.Collections;

public class CountScore : MonoBehaviour {

	int points;
	// Use this for initialization
	void Start () {
		guiText.text = points.ToString ();
		guiText.pixelOffset = new Vector2 (30 - Screen.width / 2, Screen.height / 2 - 30);
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
