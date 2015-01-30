//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//// Static class for keeping track of player statistics


//public class StatsMemory : MonoBehaviour
//{
//    private static StatsMemory instance;
//    public static StatsMemory memory
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = Object.FindObjectOfType<StatsMemory>();
//                DontDestroyOnLoad(instance.gameObject);
//            }

//            return instance;
//        }
//    }

//    public enum Scenes
//    {
//        proto,
//        StartScene
//    };
//    public class levelData
//    {
//        public int highScore;
//        public int currentTrophy;
//        public int goldLimit;
//        public int silverLimit;
//        public int bronzeLimit;
//        public bool isUnlocked;

//        public levelData(int goldLim, int silverLim, int bronzeLim)
//        {
//            goldLimit = goldLim;
//            silverLimit = silverLim;
//            bronzeLimit = bronzeLim;
//        }
//    }
//    private int currentLevelPoints;
//    private int currentTimeBonus;

//    private List<levelData> levels;


//    void Awake()
//    {
//        if(instance == null)
//        {
//            instance = this;
//            DontDestroyOnLoad(this);
//        }
//        else
//        {
//            if (this != instance)
//            {
//                DestroyImmediate(this.gameObject);
//            }
//        }
//    }
//    // Add basic level info here. When making new level, call AddLevelData() to add it to the game.
//    void Start()
//    {
//        levels = new List<levelData>();
//        AddLevelData(1000, 800, 600, 1); // testing data.

//    }
//    // Helper function for level setup
//    private void AddLevelData(int goldLimit, int silverLimit, int bronzeLimit, int levelId)
//    {
//        levels.Insert(levelId, new levelData(goldLimit, silverLimit, bronzeLimit));
//        levels[levelId].highScore = 0;
//        levels[levelId].currentTrophy = 0;
//        levels[levelId].isUnlocked = false;
//    }
//    // Clears game data but not fixed level data. 
//    public void InitNewGame()
//    {
//        foreach (levelData ld in levels)
//        {
//            ld.highScore = 0;
//            ld.currentTrophy = 0;
//            ld.isUnlocked = false;
//        }
//        Debug.Log("Levels found: " + levels.Count);
//    }

//    // To be called at game initialisation. Added level is locked by default.

//    // TODO: method for converting time to points
//    // TODO: check against out of range index; check which value is kept in list
//    // TODO: figure out initialization!   

//    // Returns integer representing trophy level, follows logic:
//    // gold = 1st place = 1,
//    // silver = 2nd place = 2,
//    // bronze = 3rd place = 3,
//    // no trophy = 4.
//    public int CompareToTrophyRequirements(int levelId)
//    {
//        int trophy = 0;
//        if (CheckRange(levelId))
//        {
//            levelData current = levels[levelId];
//            int score = current.highScore;

//            if (score > current.goldLimit)
//                trophy = 1;
//            else if (score > current.silverLimit)
//                trophy = 2;
//            else if (score > current.bronzeLimit)
//                trophy = 3;

//            SetNewTrophy(trophy, levelId);
//        }
//        return trophy;
//    }

//    public void AddToCurrentPoints(int pointsToAdd)
//    {
//        currentLevelPoints += pointsToAdd;
//    }

//    public int GetCurrentScore()
//    {
//        return currentLevelPoints;
//    }

//    // Reset level score when level is finished.
//    public void ResetScore()
//    {
//        currentLevelPoints = 0;
//        currentTimeBonus = 0;
//    }

//    // Counts time bonus points and adds them to current level score
//    // returns time bonus
//    // TODO: Figure out a decent algorithm!
//    public int CountTimeBonus(float timeInSeconds)
//    {
//        currentTimeBonus = (int)(10000 / timeInSeconds);        
//        return currentTimeBonus;
//    }

//    // Returns end score of current level, saves as highscore if better than existing one
//    // Assumes time bonus is already added to current score
//    public int CountFinalLevelScore(int levelId)
//    {
//        currentLevelPoints += currentTimeBonus;

//        if (CheckRange(levelId))        
//        {
//            SetNewHighscore(currentLevelPoints, levelId);
//        }

//        return currentLevelPoints;
//    }
//    public  int GetlevelHighScore(int levelId)
//    {
//        if (CheckRange(levelId))
//            return levels[levelId].highScore;
//        return 0;
//    }


//    // private methods assume that range check has already been made and level data exists
//    private void SetNewHighscore(int score, int levelId)
//    {
//        if (levels[levelId].highScore < score)
//        {
//            levels[levelId].highScore = score;
//        } 
//    }
//    // returns true if new trophy is  better than old one.
//    private bool SetNewTrophy(int trophy, int levelId)
//    {
//        if (trophy != 0)
//        {
//            if (trophy < levels[levelId].currentTrophy)
//            {
//                levels[levelId].currentTrophy = trophy;
//                return true;
//            }
//        }
//        return false;
//    }

//    // Checks if referred level is in the list, writes log comment if not
//    private bool CheckRange(int levelId)
//    {
//        if (levelId > levels.Count)
//        {
//            Debug.Log("Level data not added to statistics list!");
//            return false;
//        }
//        return true;
//    }
//}
