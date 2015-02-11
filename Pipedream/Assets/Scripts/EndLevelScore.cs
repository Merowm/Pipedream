using UnityEngine;
using System.Collections;

public class EndLevelScore : MonoBehaviour {

    CountScore cs;
    LevelTimer lt;
    Statistics st;
	
	void Start () {
        cs = FindObjectOfType<CountScore>();
        lt = FindObjectOfType<LevelTimer>();
        st = FindObjectOfType<Statistics>();	
	}
	


    // Called from LevelTimer at the end of a game level
    // TODO: Smooth transition!
    public void LevelFinished()
    {        
        cs.FinalLevelScore();
        
        Application.LoadLevel("EndLevel");
    }
}
