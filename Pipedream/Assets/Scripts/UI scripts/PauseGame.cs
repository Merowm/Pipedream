using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

    public GameObject overlay;

	void Start () {
        //overlay.SetActive(false);
        Time.timeScale = 1;
       
	}

    public void Pause() 
    {
        // TODO: animate?

        overlay.SetActive(true);
        Time.timeScale = 0;
    }
    //public void ResumeGame()
    //{
    //    Time.timeScale = 1;
    //}
    
}
