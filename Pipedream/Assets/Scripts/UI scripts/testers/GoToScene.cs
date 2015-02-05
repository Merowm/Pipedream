﻿using UnityEngine;
using System.Collections;

// Creates button with OnGUI method for changing scene.
// TODO: Rename script and create all buttons at the same time?

// NB!!!! This script is being used as testing ground for all kinds of GUI stuff.

public class GoToScene : MonoBehaviour {

	public int nextScene;
    public Transform textObject;
    public Texture2D buttontex;

    CountScore scoreText;
    //GameObject stats;
	void Awake ()
    {
        scoreText = FindObjectOfType<CountScore>();
        //stats = FindObjectOfType<StatsMemory>().gameObject;
	}
	
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width/2- 500, Screen.height/2 - 280, 100, 100), buttontex))
        {
            scoreText.AddScore(100);
            //StatsMemory.AddToTotalPoints(100);
            //Application.LoadLevel(nextScene);
            Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 10);
            Instantiate(textObject, pos, Quaternion.identity);

        }

    }

	// Update is called once per frame
	void Update () {
	
	}
}