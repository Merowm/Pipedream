using UnityEngine;
using System.Collections;

public class EndLevelScore : MonoBehaviour {

    CountScore cs;
    LevelTimer lt;
    Statistics st;
	// Use this for initialization
	void Start () {
        cs = FindObjectOfType<CountScore>();
        lt = FindObjectOfType<LevelTimer>();
        st = FindObjectOfType<Statistics>();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Called from LevelTimer
    // TODO: Smooth transition!
    public void LevelFinished()
    {        
        cs.FinalLevelScore();
        
        Application.LoadLevel("EndLevel");
    }
}
