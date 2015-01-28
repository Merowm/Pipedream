using UnityEngine;
using System.Collections;

public class StatsMemory : MonoBehaviour
{
    // resetScore value should always be defined when menu scene is (re)loaded!
    public bool resetScore;
    
    private int totalPoints;
    // reset this when starting a new level!
    private float timeSpentInHyperSpace;
    private float timeToDestination;

    // instance reference for duplicate checking.
    // 
    private static StatsMemory statistics;

	void Awake ()
    {
        CheckInstance();
        if (resetScore)
        {
            statistics.totalPoints = 0;
            statistics.timeSpentInHyperSpace = 0;
            statistics.timeToDestination = 0;
            
        }
    }

    void CheckInstance()
    {
        if (statistics == null)
        {
            DontDestroyOnLoad(this.gameObject);
            statistics = this;
            Debug.Log(this.totalPoints);
        }
        else if(statistics != null && statistics != this)

            Destroy(gameObject);
    }

	public void AddToTotalPoints(int pointsToAdd)
    {
        statistics.totalPoints += pointsToAdd;

        // for debugging. Visible elements should be created in each scene and player data retrieved from statistics.
        guiText.text = totalPoints.ToString();
    }

    public int GetCurrentScore()
    {
        return statistics.totalPoints;
    }

    public void SetHyperSpaceTime(float timeNeeded)
    {
        statistics.timeToDestination = timeNeeded;
    }

    public float GetRemainingHyperSpaceTime(float timeToAdd)
    {
        statistics.timeSpentInHyperSpace += timeToAdd;
        return statistics.timeToDestination - statistics.timeSpentInHyperSpace;
    }
    // For testing!

}
