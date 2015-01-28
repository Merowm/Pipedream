using UnityEngine;
using System.Collections;

public static class StatsMemory //: MonoBehaviour
{
    // resetScore value should always be defined when menu scene is (re)loaded!
    private static bool resetScore;
    
    private static int totalPoints;
    // reset this when starting a new level!
    private static float timeSpentInHyperSpace;
    private static float timeToDestination;

    // instance reference for duplicate checking.
     
    //private static StatsMemory statistics;

    //void Awake ()
    //{
    //    CheckInstance();
    //    if (resetScore)
    //    {
    //        statistics.totalPoints = 0;
    //        statistics.timeSpentInHyperSpace = 0;
    //        statistics.timeToDestination = 0;
            
    //    }
    //}

    //void CheckInstance()
    //{
    //    if (statistics == null)
    //    {
    //        DontDestroyOnLoad(this.gameObject);
    //        statistics = this;
    //        Debug.Log(this.totalPoints);
    //    }
    //    else if(statistics != null && statistics != this)

    //        Destroy(gameObject);
    //}

	public static void AddToTotalPoints(int pointsToAdd)
    {
        totalPoints += pointsToAdd;

        // for debugging. Visible elements should be created in each scene and player data retrieved from statistics.
        // guiText.text = totalPoints.ToString();
    }

    public static int GetCurrentScore()
    {
        return totalPoints;
    }

    public static void SetHyperSpaceTime(float timeNeeded)
    {
        timeToDestination = timeNeeded;
    }

    public static float GetRemainingHyperSpaceTime(float timeToAdd)
    {
        timeSpentInHyperSpace += timeToAdd;
        return timeToDestination - timeSpentInHyperSpace;
    }
    // For testing!

}
