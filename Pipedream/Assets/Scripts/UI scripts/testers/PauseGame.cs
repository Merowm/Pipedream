using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

    public Canvas overlay;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Pause() 
    {
        // animate?
        overlay.enabled = true;
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        overlay.enabled = false;
        Time.timeScale = 1;
    }
    
}
