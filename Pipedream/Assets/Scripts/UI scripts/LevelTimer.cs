using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelTimer : MonoBehaviour {

    // Leave at 0 when testing
    public int levelId;
    public float distanceMeter;
    public float updateInterval;
    public Transform playerShip;
    public float timeInSecs;
    public int fullDistance;

    Statistics stats;
    Slider distanceBar;
    Canvas UI_c;
    bool infinite;
    string timeBonus;
     
    float deltaDistance;
    float updateDelay;
    Vector3 lastPlayerPosition;
    Vector3 currentPlayerPosition;

    int bonusWithoutHit;
    int longestStreak;
    int maxBonusCount;
    Text pointsTextfield;
    Text bonusTextField;

    GooglePlayServices gps;

    KongregateAPI kong;

    Text timeTextField;
    DataSave saver;

	
	void Awake ()
    {
        distanceMeter = 0;
        updateDelay = 0;
        timeInSecs = 0;

        gps = GameObject.FindObjectOfType<GooglePlayServices>();

        if (levelId == 99)
            infinite = true;    
	}
	void Start()
    {           
        stats = GameObject.FindWithTag("statistics").GetComponent<Statistics>();
        saver = GameObject.FindWithTag("statistics").GetComponent<DataSave>();
        Debug.Log(Difficulty.currentDifficulty);
        // reset temp scores, just in case
        stats.ResetScore();
        
        playerShip = GameObject.FindWithTag("Player").GetComponent<Transform>();

        distanceBar = GameObject.FindWithTag("travelIndicator").GetComponent<Slider>();

        timeTextField = GameObject.Find("timeText").GetComponent<Text>();

        if (!infinite)
        {
            if (levelId > 0)
            {
                fullDistance = stats.GetLevelDistance(levelId);
                maxBonusCount = stats.GetMaxBonusAmount(levelId);
                stats.SetLevelPlayed(levelId);
                GameObject.Find("timePanel").SetActive(false);
                GameObject.Find("levelTextText").GetComponent<Text>().text = "level # " + levelId.ToString();
            }
            else
            {
                fullDistance = 1000;
                maxBonusCount = 100;
                Debug.Log("Timer in testing mode");
            }
        }
        else
        {
            stats.SetLevelPlayed(levelId);
            distanceBar.gameObject.SetActive(false);
            if (Difficulty.currentDifficulty == Difficulty.DIFFICULTY.normal)
                timeBonus = "20";
            else timeBonus = "15";
        }

        pointsTextfield = GameObject.FindWithTag("Scoretext").GetComponent<Text>();
        WriteToGuiPoints(0);

        bonusTextField = GameObject.FindWithTag("Bonustext").GetComponent<Text>();
        WriteToGuiBonus(0);
        
        lastPlayerPosition = playerShip.position;
    }
	
    
	void Update ()
    {
        timeInSecs += Time.deltaTime;

        if (!infinite)
        {
            updateDelay += Time.deltaTime;

            // TODO: Change to use percentage of full distance? (why?)
            if (updateDelay >= updateInterval)
            {
                currentPlayerPosition = playerShip.position;
                deltaDistance = currentPlayerPosition.z - lastPlayerPosition.z;
                // save travelled distance at jump
                // NB! only works if player position jumps to (*,*,0)!
                if (deltaDistance < 0)
                {
                    distanceMeter += lastPlayerPosition.z;
                    Debug.Log("jumped at point " + lastPlayerPosition.z);
                }
                distanceMeter += deltaDistance;

                distanceBar.value = (distanceMeter / fullDistance);

                lastPlayerPosition = currentPlayerPosition;
                updateDelay = 0;
            }
            if (distanceMeter >= fullDistance && levelId > 0)
            {
                Debug.Log("level length: " + timeInSecs + "seconds");
                FinalLevelScore();
                if (levelId < 6)
                    stats.UnlockLevel(levelId + 1);
                Application.LoadLevel("EndLevel");
            }
        }
        else if (timeInSecs > 5)
        {
            // update timer GUI            
            timeTextField.text = ((int)timeInSecs - 5).ToString();
        }
        
	}

    void WriteToGuiPoints(int score)
    {
        pointsTextfield.text = score.ToString();
    }
    private void WriteToGuiBonus(int collected)
    {        
        if (infinite)
        {
           
            bonusTextField.text = collected.ToString();
        }
        else bonusTextField.text = collected.ToString() + " / " + maxBonusCount;
    }

    // Called from bonus object when triggered. Changes GUI text.
    public void AddScore(int newPoints)
    {
        stats.AddToCurrentPoints(newPoints);
        WriteToGuiPoints(stats.GetCurrentScore());        
    }
    public void GotSpecial()
    {
        stats.SpecialAcquired();
    }

    public void ContinueBonusStreak(bool gotMoreBonus)
    {
        
        if (gotMoreBonus)
        {
            stats.AddToBonusCount();
            WriteToGuiBonus(stats.GetCurrentBonus());
            ++bonusWithoutHit;
            if (longestStreak < bonusWithoutHit)
                longestStreak = bonusWithoutHit;

        }
        else
        {
            stats.AddToHitCount();
            if (longestStreak < bonusWithoutHit)
                longestStreak = bonusWithoutHit;
            bonusWithoutHit = 0;
        }
    }

    // Called when level ends
    public void FinalLevelScore()
    {
        stats.SetCurrentStreak(longestStreak);
        stats.SetFinalLevelScore(levelId);
        // also saves best trophy
        int medal = stats.CompareToTrophyRequirements(levelId);        
        // sends new best kongregate statistics
        if (KongregateAPI.Connected)
        {
            // submit level data to kong
            KongregateAPI.SubmitData("gameFinishedOnNormal", stats.GetAllFinished());
            KongregateAPI.SubmitData("levelsFinishedOnNormal", stats.GetFinishedOnNormalCount());
        }
#if UNITY_ANDROID
        if (medal == 1)//if gold
        {
            gps.UnlockAchievement(levelId, GooglePlayServices.ACHIEVEMENT_TYPE.GOLD);
        }
#endif
    }

    // if needed to call from elsewhere / next mode NOT starting from same z point as last!
    public void SaveDistanceAtJump(float distanceTravelledAtCurrentMode)
    {
        distanceMeter += distanceTravelledAtCurrentMode;
    }

    // Called from other scorekeeping objects.
    public int GetCurrentLevel()
    {
        return levelId;
    }

    public float GetCurrentDistance()
    {
        return distanceMeter;
    }
    public void Gameover(GameObject res)
    {        
        res.transform.FindChild("time").GetComponent<Text>().text = (((int)timeInSecs - 5).ToString() + "\n X " + timeBonus);
        res.transform.FindChild("score").GetComponent<Text>().text = stats.GetCurrentScore().ToString();
        res.transform.FindChild("collected").GetComponent<Text>().text = stats.GetCurrentBonus().ToString();
        // save endless results & mark personal best
        stats.UpdateEndlessRecord((int)timeInSecs - 5);
        res.transform.FindChild("timeBest").GetComponent<Text>().text = stats.GetSecsSurvived().ToString();
        res.transform.FindChild("scoreBest").GetComponent<Text>().text = stats.GetBestScore().ToString();
        saver.SaveEndlessScore();
    }
    public bool IsInfinite()
    {
        return infinite;
    }
}
