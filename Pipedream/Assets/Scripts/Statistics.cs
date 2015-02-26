using UnityEngine;
using UnityEngine.UI;
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
    private int currentLongestBonusStreak;

    private int lastPlayedLevel;
    private int lastVisitedPlanet;
    private int lastLevelTrophy;
    private bool special;

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
        public float timeToRace;

        public levelData(int goldLim, int silverLim, int bronzeLim,int bonusAmount, int levelLength, float levelTime, int levelId)
        {
            goldLimit = goldLim;
            silverLimit = silverLim;
            bronzeLimit = bronzeLim;
            bonusCount = bonusAmount;
            levelID = levelId;
            distanceToRace = levelLength;
            timeToRace = levelTime;
            highScore = 0;
            currentTrophy = 0;
            isUnlocked = false;
        }
    }
    public List<levelData> levels;

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
        AddLevelData(405, 4, 30, 16, 1); // testing data.
        AddLevelData(5, 5, 5, 5, 2); // testing unlocking.
        AddLevelData(5, 5, 5, 5, 3);
        AddLevelData(5, 5, 5, 5, 4);
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

    public void SetCurrentStreak(int streakLength)
    {
        currentLongestBonusStreak = streakLength;
    }

    public void SpecialAcquired()
    {
        special = true;
    }

    public void SetVisitedPlanet(int planet)
    {
        lastVisitedPlanet = planet;
    }

    
    // Reset temp level score when level is finished.
    public void ResetScore()
    {
        currentLevelPoints = 0;
        currentBonusAmount = 0;
        currentObstaclesHit = 0;
        currentLongestBonusStreak = 0;
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

    public int GetLevelTrophy(int levelId)
    {
        return FindLevel(levelId).currentTrophy;
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

    public int GetLongestStreak()
    {
        return currentLongestBonusStreak;
    }

    public bool GetSpecialAcquired()
    {
        return special;
    }

    public int GetLastPlanet()
    {
        return lastVisitedPlanet;
    }

    public bool GetAvailability(int levelId)
    {
        levelData lv = FindLevel(levelId);

        if (lv != null)
        {
            return lv.isUnlocked;
        }
        return false;
    }

    public float GetLevelTime(int levelId)
    {
        return FindLevel(levelId).timeToRace;
    }

 
    //////////////////////////////////////
    // Helper methods
    //////////////////////////////////////

    // Finds level data with Id. Returns null if not found, which usually throws error.
    public levelData FindLevel(int levelId)
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
    private void AddLevelData(int bonusAmount, int bonusItemCount, int levelLength, float levelTime, int levelId)
    {
        int goldLimit = (int)(bonusAmount * 0.85f);
        int silverLimit = (int)(bonusAmount * 0.5f);
        int bronzeLimit = (int)(bonusAmount * 0.2f);
        levels.Add(new levelData(goldLimit, silverLimit, bronzeLimit, bonusItemCount, levelLength, levelTime, levelId));
    }

    // returns true if new trophy is  better than old one and saves it in the level data. 
    private bool SetNewTrophy(int trophy, levelData level)
    {
        if (trophy != 0)
        {
            if (level.currentTrophy == 0)
            {
                level.currentTrophy = trophy;
                return true;
            }
            else if (trophy < level.currentTrophy)
            {
                level.currentTrophy = trophy;
                Debug.Log("Set highest trophy to " + trophy);
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

    // coordinates conversion (because nothing else seems to work right...)
    public Vector3 ScreenToCanvasPoint(Canvas canvas, Vector3 point)
    {
        Vector3 temp = new Vector3(0, 0, 0);
        float uiWidth;
        float uiHeight;
        CanvasScaler sc = canvas.GetComponent<CanvasScaler>();
        uiWidth = sc.referenceResolution.x;
        uiHeight = sc.referenceResolution.y;
        temp.x = (point.x / Screen.width * uiWidth) - (uiWidth / 2);
        temp.y = (point.y / Screen.height * uiHeight) - (uiHeight / 2);
        
        return temp;
    }
}