using UnityEngine;
using System.Collections;

// Static class for keeping track of player statistics

public static class StatsMemory
{
    private static int totalPoints;
    private static int fuelPoints;
    // TODO: method for converting time to points
    // Hyperspace time req not req!
    // Trophy limits set as an array? 

	public static void AddToTotalPoints(int pointsToAdd)
    {
        totalPoints += pointsToAdd;
    }

    public static int GetCurrentScore()
    {
        return totalPoints;
    }

    public static void ResetScore()
    {
        totalPoints = 0;
    }

    public static int CountFuelPoints(int fuelToAdd)
    {
        return fuelPoints += fuelToAdd;
    }
}
