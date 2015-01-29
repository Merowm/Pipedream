using UnityEngine;
using System.Collections;

public class CountScore : MonoBehaviour {

	// Scorekeeping script for GUIText that shows current score in level and updates statistics memory
    public int goldLimit;
    public int silverLimit;
    public int bronzeLimit;
    public int levelId;

    void Awake()
    {
        StatsMemory.AddLevelTrophyLimits(goldLimit, silverLimit, bronzeLimit, levelId);
    }
	void Update () {
	
	}
	public void AddScore(int newPoints)
	{
        StatsMemory.AddToTotalPoints(newPoints);
		guiText.text = StatsMemory.GetCurrentScore().ToString ();
        Debug.Log(guiText.text);
	}
    public void FinalLevelScore()
    {
        StatsMemory.CountlevelScore(levelId);
        StatsMemory.ResetScore();
    }
       
}
