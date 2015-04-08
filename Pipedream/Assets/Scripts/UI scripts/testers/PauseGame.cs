using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

    public GameObject overlay;

	// Use this for initialization
	void Start () {
        overlay.SetActive(false);
        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Pause() 
    {
        // animate?
        
        overlay.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        //overlay.enabled = false;
        Time.timeScale = 1;
    }
    
}
