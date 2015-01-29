using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Static class for keeping track of player statistics

public static class StatsMemory
{
    private static int levelPoints;
    private static List<int> levelScores;
    private static List<int> leveltrophies;
    private static List<int[]> trophyLimits;

    // TODO: method for converting time to points
    // TODO: check against out of range index; check which value is kept in list
    // TODO: figure out initialization!

    public static void AddLevelTrophyLimits(int gold, int silver, int bronze, int levelId)
    {
        trophyLimits[levelId] = new int[3]{gold, silver, bronze};
    }

    // Returns integer representing trophy level, follows logic:
    // gold = 1st place = 1,
    // silver = 2nd place = 2,
    // bronze = 3rd place = 3,
    // no trophy = 4.
    public static int CompareToTrophyRequirements(int levelId)
    {
        int trophy;
        int[] limits = trophyLimits[levelId];
        int score = levelScores[levelId];
        if (score > limits[0])
            trophy = 1;
        else if (score > limits[1])
            trophy = 2;
        else if (score > limits[2])
            trophy = 3;
        else trophy = 4;
        SetNewTrophy(trophy, levelId);
        return trophy;
    }

	public static void AddToTotalPoints(int pointsToAdd)
    {
        levelPoints += pointsToAdd;
    }

    public static int GetCurrentScore()
    {
        return levelPoints;
    }

    // Reset level score when level is finished.
    public static void ResetScore()
    {
        levelPoints = 0;
    }
    // Counts time bonus points and adds them to current level score
    // TODO: Figure out a decent algorithm!
    public static int CountTimeBonus(float timeInSeconds)
    {
        int bonusPoints = (int)(10000 / timeInSeconds);
        levelPoints += bonusPoints;
        return bonusPoints;
    }

    // TODO: check older score first and compare?
    public static int CountlevelScore(int levelId)
    {
       
        levelScores[levelId] = levelPoints;
        return levelPoints;
    }
    public static int GetlevelScore(int levelId)
    {
        if (levelId > levelScores.Count)
            return 0;
        return levelScores[levelId];
    }

    private static void SetNewTrophy(int trophy, int levelId)
    {
        if (trophy < leveltrophies[levelId])
            leveltrophies[levelId] = trophy;        
    }
}
