using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Statistics : MonoBehaviour 
{
    private static Statistics instance;
    public static Statistics memory
    {
        get
        {
            if (instance == null)
            {
                instance = Object.FindObjectOfType<Statistics>();
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }
    private int currentLevelPoints;
    private int currentTimeBonus;

    public class levelData
    {
        public int levelID;
        public int highScore;
        public int currentTrophy;
        public int goldLimit;
        public int silverLimit;
        public int bronzeLimit;
        public bool isUnlocked;

        public levelData(int goldLim, int silverLim, int bronzeLim, int levelId)
        {
            goldLimit = goldLim;
            silverLimit = silverLim;
            bronzeLimit = bronzeLim;
            levelID = levelId;
            highScore = 0;
            currentTrophy = 0;
            isUnlocked = false;
        }
    }
    private List<levelData> levels;

	void Awake ()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
            {
                DestroyImmediate(this.gameObject);
            }
        }
	}
    // Add basic level info here. When making new level, call AddLevelData() to add it to the game.
    void Start()
    {
        levels = new List<levelData>();
        AddLevelData(1000, 800, 600, 1); // testing data.

    }

    // Update statistics temp data (if needed)
    public void AddToCurrentPoints(int pointsToAdd)
    {
        currentLevelPoints += pointsToAdd;
    }

    public int GetCurrentScore()
    {
        return currentLevelPoints;
    }

    public int CountFinalLevelScore(int levelId)
    {
        currentLevelPoints += currentTimeBonus;
        levelData level = FindLevel(levelId);
        if (level != null)
        {
            SetNewHighscore(currentLevelPoints, level);
        }
        return currentLevelPoints;
    }

    public int GetlevelHighScore(int levelId)
    {
        return FindLevel(levelId).highScore;
    }
    // Reset temp level score when level is finished.
    public void ResetScore()
    {
        currentLevelPoints = 0;
        currentTimeBonus = 0;
    }

    // Resets player data but not basic level info
    public void ResetGame()
    {
        foreach (levelData ld in levels)
        {
            ld.highScore = 0;
            ld.currentTrophy = 0;
            ld.isUnlocked = false;
        }
        Debug.Log("Levels found: " + levels.Count);
    }

    public void UnlockLevel(int levelId)
    {
        FindLevel(levelId).isUnlocked = true;
    }

    // Returns integer representing trophy level, follows logic:
    // gold = 1st place = 1,
    // silver = 2nd place = 2,
    // bronze = 3rd place = 3,
    // no trophy = 0.
    public int CompareToTrophyRequirements(int levelId)
    {
        int trophy = 0;
        levelData current = FindLevel(levelId);
        if (current != null)
        {
            int score = currentLevelPoints;

            if (score > current.goldLimit)
                trophy = 1;
            else if (score > current.silverLimit)
                trophy = 2;
            else if (score > current.bronzeLimit)
                trophy = 3;

            SetNewTrophy(trophy, current);
        }
        return trophy;
    }

    // Counts time bonus points and adds them to current level score
    // returns time bonus
    // TODO: Figure out a decent algorithm!
    public int CountTimeBonus(float timeInSeconds)
    {
        currentTimeBonus = (int)(10000 / timeInSeconds);
        return currentTimeBonus;
    }

    //////////////////////////////////////
    // Helper methods
    //////////////////////////////////////

    // Finds level data with Id. Returns null if not found, which usually throws error.
    private levelData FindLevel(int levelId)
    {
        foreach (levelData ld in levels)
        {
            if (ld.levelID == levelId)
            {                
                return ld;
            }
        }
        Debug.Log("Level not added to levels list!");
        return null;
    }

    // levelId should be scene number in project, so it can be used to call Application.LoadLevel().
    private void AddLevelData(int goldLimit, int silverLimit, int bronzeLimit, int levelId)
    {
        levels.Add(new levelData(goldLimit, silverLimit, bronzeLimit, levelId));
    }

    // returns true if new trophy is  better than old one and saves it in the level data. 
    private bool SetNewTrophy(int trophy, levelData level)
    {
        if (trophy != 0)
        {
            if (trophy < level.currentTrophy)
            {
                level.currentTrophy = trophy;
                return true;
            }
        }
        return false;
    }

    // private methods assume that range check has already been made and level data exists
    private void SetNewHighscore(int score, levelData level)
    {
        if (level.highScore < score)
        {
            level.highScore = score;
        }
    }
}