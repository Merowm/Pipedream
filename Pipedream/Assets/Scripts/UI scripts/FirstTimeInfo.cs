using UnityEngine;
using System.Collections;

public class FirstTimeInfo : MonoBehaviour {

    public GameObject info;
    public GameObject backButton;
    GoToNext gonext;
    VolControl sounds;
    //public bool showTutorial;

    // for testing
    public float showtime;

	void Start () 
    {
        if (gonext = GetComponent<GoToNext>()) {}
        else Debug.Log("FirstTimeInfo: gotonext not found");
        if (sounds = GameObject.FindWithTag("statistics").GetComponent<VolControl>()) { }
        else Debug.Log("FirstTimeInfo: volcontrol not found");
	}

    public void ShowAndGO()
    {
        sounds.PlayButtonEffect();
        if (sounds.tutorialIsOn)
        {
            info.SetActive(true);
            backButton.SetActive(false);
            Invoke("Go", showtime);
        }
        else gonext.GoToScene();
    }
    void Go()
    {
        gonext.Go();
    }
}
