using UnityEngine;
using System.Collections;

public class CountScore : MonoBehaviour {

	// Scorekeeping script for GUIText that shows current score in level and updates statistics memory

    //public StatsMemory.Scenes scene;
    GameObject statObj;
    Statistics stats;
    public int levelId;

    void Start()
    {
        statObj = GameObject.FindGameObjectWithTag("statistics");
        if (statObj != null)
            stats = statObj.GetComponent<Statistics>();
    }
	void Update () {
	
	}

    // Called from bonus object when triggered
	public void AddScore(int newPoints)
	{
        stats.AddToCurrentPoints(newPoints);
        guiText.text = stats.GetCurrentScore().ToString ();
        Debug.Log(guiText.text);
	}

    // Called when level ends
    public void FinalLevelScore()
    {
        guiText.text = stats.CountFinalLevelScore(levelId).ToString();
        Debug.Log("highest: " + stats.GetlevelHighScore(levelId));
        // also saves best trophy
        int medal = stats.CompareToTrophyRequirements(levelId);
        // for testing purposes
        switch (medal)
        {
            case 1:
                Debug.Log("You got gold!");
                break;
            case 2:
                Debug.Log("You got silver!");
                break;
            case 3:
                Debug.Log("You got bronze!");
                break;
            default:
                Debug.Log("No medal!");
                break;
        }
        stats.ResetScore();        
    }
       
}
