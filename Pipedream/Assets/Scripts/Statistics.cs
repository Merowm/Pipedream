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
    private int currentBonusAmount;
    private int currentObstaclesHit;

    private int lastPlayedLevel;
    private int lastLevelTrophy;

    public class levelData
    {
        public int levelID;
        public int highScore;
        public int currentTrophy;
        public int goldLimit;
        public int silverLimit;
        public int bronzeLimit;
        public bool isUnlocked;
        public int bonusCount;
        public int distanceToRace;

        public levelData(int goldLim, int silverLim, int bronzeLim,int bonusAmount, int levelLength, int levelId)
        {
            goldLimit = goldLim;
            silverLimit = silverLim;
            bronzeLimit = bronzeLim;
            bonusCount = bonusAmount;
            levelID = levelId;
            distanceToRace = levelLength;
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


        // TODO: Move level data setup back to Start(). This initializes levels and should happen (only) in menu scene.
        // Moved to Awake() for testing reasons.
        levels = new List<levelData>();
        AddLevelData(40, 4, 500, 1); // testing data.
	}


    // Add basic level info here. When making new level, call AddLevelData() to add it to the game.
    void Start()
    {


    }

    // Update statistics temp data (if needed)
    public void AddToCurrentPoints(int pointsToAdd)
    {
        currentLevelPoints += pointsToAdd;
        if (currentLevelPoints < 0)
        {
            currentLevelPoints = 0;
        }
    }

    public void AddToBonusCount()
    {
        ++currentBonusAmount;
    }

    public void AddToHitCount()
    {
        ++currentObstaclesHit;
    }

    public void SetLevelPlayed(int levelId)
    {
        lastPlayedLevel = levelId;
    }

    public int SetFinalLevelScore(int levelId)
    {
        levelData level = FindLevel(levelId);
        if (level != null)
        {
            SetNewHighscore(currentLevelPoints, level);
        }
        return currentLevelPoints;
    }

    
    // Reset temp level score when level is finished.
    public void ResetScore()
    {
        currentLevelPoints = 0;
        currentBonusAmount = 0;
        currentObstaclesHit = 0;
    }

    // Resets player data but not basic level info. For starting new game without exiting.
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
    // also saves best trophy.
    public int CompareToTrophyRequirements(int levelId)
    {
        lastLevelTrophy = 0;
        levelData current = FindLevel(levelId);
        if (current != null)
        {
            int score = currentLevelPoints;

            if (score > current.goldLimit)
                lastLevelTrophy = 1;
            else if (score > current.silverLimit)
                lastLevelTrophy = 2;
            else if (score > current.bronzeLimit)
                lastLevelTrophy = 3;

            SetNewTrophy(lastLevelTrophy, current);
        }
        return lastLevelTrophy;
    }

    //////////////////////////////////////
    // Get methods
    //////////////////////////////////////
    public int GetCurrentLevel()
    {
        return lastPlayedLevel;
    }
    public string GetLevelNameAsString(int levelId)
    {        
        return ("level" + levelId).ToString();
    }
    public int GetCurrentScore()
    {
        return currentLevelPoints;
    }

    public int GetCurrentBonus()
    {
        return currentBonusAmount;
    }

    public int GetCurrentHitCount()
    {
        return currentObstaclesHit;
    }

    public int GetCurrentTrophy()
    {
        return lastLevelTrophy;
    }

    public int GetMaxBonusAmount(int levelId)
    {
        return FindLevel(levelId).bonusCount;
    }

    public int GetlevelHighScore(int levelId)
    {
        return FindLevel(levelId).highScore;
    }

    public int GetLevelDistance(int levelId)
    {
        return FindLevel(levelId).distanceToRace;
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

    // For adding a level to game.
    private void AddLevelData(int bonusAmount, int bonusItemCount, int levelLength, int levelId)
    {
        int goldLimit = (int)(bonusAmount * 0.85f);
        int silverLimit = (int)(bonusAmount * 0.5f);
        int bronzeLimit = (int)(bonusAmount * 0.2f);
        levels.Add(new levelData(goldLimit, silverLimit, bronzeLimit, bonusItemCount, levelLength, levelId));
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