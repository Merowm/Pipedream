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
    public Color32[] colors = new Color32[9];
    public Color32[] defaultColors;
    public bool hasCustoms = false;
    private DataSave data;
    

    private int currentLevelPoints;
    private int currentBonusAmount;
    private int currentObstaclesHit;
    private int currentLongestBonusStreak; // remove if not needed
    
    private int lastPlayedLevel;
    private int lastVisitedPlanet;
    private int lastLevelTrophy;
    private bool special;
    // endless mode scores
    private int secondsSurvived;
    private int bestPoints;
    private int bestCollected;

    // stats to send to kongregate
    private int goldMedalsCount;
    private int levelsFinishedOnNormal;    
    private int gameFinishedOnNormal;
    private int endlessSurviveTimeOnNormal;
    private int extraHonorsEarned;

    public class levelData
    {
        public int levelID;
        public int highScore;
        public int currentTrophy;
        public int maxPoints;
        public int goldLimit;
        public int silverLimit;
        public int bronzeLimit;
        public bool isUnlocked;
        public int bonusCount;
        public int distanceToRace;
        public float timeToRace;
        public bool allCollected;
        public bool nothingHit;
        public bool finishedOnNormal;
        public bool specialFound;
        public bool fullPoints;

        public levelData(int maxpoints, int goldLim, int silverLim, int bronzeLim,int bonusAmount, int levelLength, float levelTime, int levelId)
        {
            goldLimit = goldLim;
            silverLimit = silverLim;
            bronzeLimit = bronzeLim;
            maxPoints = maxpoints;
            bonusCount = bonusAmount;
            levelID = levelId;
            distanceToRace = levelLength;
            timeToRace = levelTime;
            highScore = 0;
            currentTrophy = 0;
            isUnlocked = false;
            allCollected = false;
            nothingHit = false;
            finishedOnNormal = false;
            specialFound = false;
            fullPoints = false;
        }
    }
    public List<levelData> levels;

    private GooglePlayServices gps;

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
#if UNITY_ANDROID
        gps = FindObjectOfType<GooglePlayServices>();
#endif

        // Define default colors!
        defaultColors = new Color32[9];
        defaultColors[0] = new Color32(0, 215, 255, 255);
        defaultColors[1] = new Color32(0, 215, 255, 255);
        defaultColors[2] = new Color32(255, 255, 215, 255);
        defaultColors[3] = new Color32(140, 150, 175, 255);
        defaultColors[4] = new Color32(80, 80, 255, 255);
        defaultColors[5] = new Color32(150, 225, 150, 170);
        defaultColors[6] = new Color32(255, 200, 0, 255);
        defaultColors[7] = new Color32(255, 0, 0, 255);
        defaultColors[8] = new Color32(255, 35, 0, 255);
        // TODO: Move level data setup back to Start(). This initializes levels and should happen (only) in menu scene.
        // Moved to Awake() for testing reasons.
        levels = new List<levelData>();
        AddLevelData(2700, 23, 5500, 56, 2); 
        AddLevelData(2000, 16, 4000, 40, 1); // work in progress 
        AddLevelData(2400, 20, 4800, 48, 3);
        AddLevelData(2700, 23, 5500, 56, 4);
        AddLevelData(2700, 23, 5500, 56, 5);
        AddLevelData(3700, 33, 7500, 74, 6);//temp
	}
    // Add basic level info here. When making new level, call AddLevelData() to add it to the game.
    // BonusAmount is the max score for the level; 
    // levelLength is the total distance player has to move along z axis;
    // levelTime is the playing time of level in seconds.
    void Start()
    {
        data = transform.GetComponent<DataSave>();
        if (!hasCustoms)
            colors = defaultColors;
    }

    // Update statistics temp data 
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
            if (Difficulty.currentDifficulty == Difficulty.DIFFICULTY.normal)
            {
                if (!level.specialFound)
                    level.specialFound = special;
                if (!level.nothingHit)
                    level.nothingHit = (currentObstaclesHit == 0);
                if (!level.finishedOnNormal)
                    level.finishedOnNormal = true;
                if (!level.allCollected)
                    level.allCollected = (currentBonusAmount == level.bonusCount);
                if (!level.fullPoints)
                    level.fullPoints = (currentLevelPoints == level.maxPoints);
            }
        }
        data.SaveScores();
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
            ld.allCollected = false;
            ld.nothingHit = false;
            ld.finishedOnNormal = false;
            ld.specialFound = false;
            ld.fullPoints = false;
        }
        secondsSurvived = 0;
        bestPoints = 0;
        bestCollected = 0;
        goldMedalsCount = 0;
        levelsFinishedOnNormal = 0;    
        gameFinishedOnNormal = 0;
        endlessSurviveTimeOnNormal = 0;
        extraHonorsEarned = 0;
        // unlock first lvl
        UnlockLevel(1);
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

    public void UpdateEndlessRecord(int secs)
    {
        if (secondsSurvived < secs)
            secondsSurvived = secs;
        if (Difficulty.currentDifficulty == Difficulty.DIFFICULTY.normal && endlessSurviveTimeOnNormal < secs)
            endlessSurviveTimeOnNormal = secs;
        if (bestPoints < currentLevelPoints)
        {
            bestPoints = currentLevelPoints;
#if UNITY_ANDROID
        if (gps != null)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                // handle success or failure
                if (success)
                {
                    gps.UpdateLeaderboard(currentLevelPoints);
                    Social.ShowLeaderboardUI();
                }
            });
        }
#endif
        }
        if (bestCollected < currentBonusAmount)
            bestCollected = currentBonusAmount;
    }
    // for loading scores from save file
    public void SetBestTime(int time)
    {
        secondsSurvived = time;
    }
    public void SetBestNormalTime(int time)
    {
        endlessSurviveTimeOnNormal = time;
    }
    public void SetBestPoints(int points)
    {
        bestPoints = points;
    }
    public void SetBestCollected(int collected)
    {
        bestCollected = collected;
    }

    public Color32[] GetCustoms()
    {
        return colors;
    }
    public Color32[] GetDefaultCustoms()
    {
        return defaultColors;
    }
    public void SaveCustoms(Color32[] col)
    {
        for (int i = 0; i < colors.Length; ++i)
        {
            colors[i].a = col[i].a;
            colors[i].r = col[i].r;
            colors[i].g = col[i].g;
            colors[i].b = col[i].b;
        }
        hasCustoms = true;
        data.SetSettings();
    }
    public void SetlevelsFinished(int amount)
    {
        levelsFinishedOnNormal = amount;
    }
    public void SetGameFinished(int finished)
    {
        gameFinishedOnNormal = finished;
    }
    public void SetGold(int gold)
    {
        goldMedalsCount = gold;
    }
    public void SetExtras(int amount)
    {
        extraHonorsEarned = amount;
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
        if (levelId == 99)
            return "infinite";
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
        if (levelId == 99)
            return true;
        levelData lv = FindLevel(levelId);

        if (lv != null)
        {
            return lv.isUnlocked;
        }
        return false;
    }

    // specials
    public bool HasAllCollected(int levelId)
    {
        return FindLevel(levelId).allCollected;
    }
    public bool HasNothingHit(int levelId)
    {
        return FindLevel(levelId).nothingHit;
    }
    public bool HasFinishedOnNormal(int levelId)
    {
        return FindLevel(levelId).finishedOnNormal;
    }
    public bool HasSpecialFound(int levelId)
    {
        return FindLevel(levelId).specialFound;
    }
    public bool HasMaxPoints(int levelId)
    {
        return FindLevel(levelId).fullPoints;
    }
    // endless mode
    public int GetSecsSurvived()
    {
        return secondsSurvived;
    }
    public int GetSecsSurvivedNormal()
    {
        return endlessSurviveTimeOnNormal;
    }
    public int GetBestScore()
    {
        return bestPoints;
    }
    public int GetBestCollected()
    {
        return bestCollected;
    }

    public int GetAllGold()
    {
        int gold = 0;
        foreach (levelData l in levels)
        {
            if (l.currentTrophy == 1)
                ++gold;
        }
        goldMedalsCount = gold;
        return gold;
    }
    public int GetAllExtras()
    {
        int ext = 0;
        foreach (levelData l in levels)
        {
            if (l.allCollected)
                ++ext;
            if (l.finishedOnNormal)
                ++ext;
            if (l.nothingHit)
                ++ext;
            if (l.specialFound)
                ++ext;
        }
        extraHonorsEarned = ext;
        return ext;
    }
    public int GetFinishedOnNormalCount()
    {
        int temp = 0;
        foreach (levelData l in levels)
        {
            if (l.finishedOnNormal)
                temp++;
        }
        levelsFinishedOnNormal = temp;
        return temp;
    }
    public int GetAllFinished()
    {
        int temp = 0;
        if (levelsFinishedOnNormal == levels.Count)
            temp = 1;
        return temp;
    }
    public int GetLevelMaxpoints(int level)
    {
        return FindLevel(level).maxPoints;
    }
    // for getting level time (debugging only)
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
        levels.Add(new levelData(bonusAmount, goldLimit, silverLimit, bronzeLimit, bonusItemCount, levelLength, levelTime, levelId));
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
    // Converts (secs) seconds into string which shows minutes and seconds as "00:00"
    public string TextWithMinutes(int secs)
    {
        string txt;
        if (secs % 60 > 9)
            txt = ((secs / 60).ToString() + ":" + (secs % 60).ToString());
        else
            txt = ((secs / 60).ToString() + ":0" + (secs % 60).ToString());
        return txt;
    }
}