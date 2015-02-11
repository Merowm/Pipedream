using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountScore : MonoBehaviour {

	// Scorekeeping script for GUIText that shows current score in level and updates statistics memory

    int levelId;
    int bonusWithoutHit;

    GameObject statObj;
    Statistics stats;
    LevelTimer levelControl;

    Text textfield;

    void Start()
    {
        levelControl = FindObjectOfType<LevelTimer>();
        levelId = levelControl.GetCurrentLevel();

        statObj = GameObject.FindGameObjectWithTag("statistics");
        if (statObj != null)
            stats = statObj.GetComponent<Statistics>();

        textfield = GameObject.FindWithTag("Scoretext").GetComponent<Text>();
        WriteToGui(0);
        bonusWithoutHit = 0;
    }
	void Update () {
	
	}

    // Called from bonus object when triggered. Changes GUI text.
	public void AddScore(int newPoints)
	{
        stats.AddToCurrentPoints(newPoints);
        WriteToGui(stats.GetCurrentScore());
	}

    public void ContinueBonusStreak(bool gotMoreBonus)
    {
        if (gotMoreBonus)
        {
            ++bonusWithoutHit;
        }
        else
        {
            bonusWithoutHit = 0;
        }
    }

    // Called when level ends
    public void FinalLevelScore()
    {
        //guiText.text = stats.SetFinalLevelScore(levelId).ToString();
        Debug.Log("highest: " + stats.GetlevelHighScore(levelId));
        // also saves best trophy
        int medal = stats.CompareToTrophyRequirements(levelId);

        /////////////////////////////////////////////////// for testing purposes
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
        //////////////////////////////////////////////////// end testing
        stats.ResetScore();        
    }
    
    void WriteToGui(int score)
    {
        textfield.text = score.ToString();
    }
}
