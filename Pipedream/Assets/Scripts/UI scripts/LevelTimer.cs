using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelTimer : MonoBehaviour {

    // Leave at 0 when testing
    public int levelId;
    public float distanceMeter;
    public float updateInterval;
    public Transform playerShip;
    public GameObject distance;
    public float timeInSecs;

    Statistics stats;
    EndLevelScore end;
    GameObject instantBar;
    Slider distanceBar;
    scrollbartest testbar;
    Canvas UI_c;
    public int fullDistance;
     
    float deltaDistance;
    float updateDelay;
    Vector3 lastPlayerPosition;
    Vector3 currentPlayerPosition;

    int bonusWithoutHit;
    int longestStreak;
    int maxBonusCount;
    Text pointsTextfield;
    Text bonusTextField;
	
	void Awake ()
    {
        distanceMeter = 0;
        updateDelay = 0;
        timeInSecs = 0;
        
	}
	void Start()
    {   
        // Components set to variables, now with debug checks!
        stats = GameObject.FindWithTag("statistics").GetComponent<Statistics>();
        if (stats != null)
        {
            Debug.Log("Timer found stats by tag");
        }

        // For debugging and testing, no level data defined, no ending
        if (levelId > 0)
        {
            fullDistance = stats.GetLevelDistance(levelId);
            maxBonusCount = stats.GetMaxBonusAmount(levelId);
            stats.SetLevelPlayed(levelId);
        }
        else
        {
            fullDistance = 1000;
            maxBonusCount = 100;
            Debug.Log("Timer in testing mode");
        }

        
        playerShip = GameObject.FindWithTag("Player").GetComponent<Transform>();
        if (playerShip != null)
        {
            Debug.Log("Timer found player by tag");
        }

        CreateDistanceBar();

        //testbar = GameObject.FindWithTag("travelIndicator").GetComponent<scrollbartest>();

        pointsTextfield = GameObject.FindWithTag("Scoretext").GetComponent<Text>();
        if (pointsTextfield != null)
        {
            Debug.Log("Timer found pointsTextfield by tag");
            WriteToGuiPoints(0);
        }

        bonusTextField = GameObject.FindWithTag("Bonustext").GetComponent<Text>();
        if (bonusTextField != null)
        {
            Debug.Log("Timer found bonusTextField by tag");
            WriteToGuiBonus(0);
        }

        lastPlayerPosition = playerShip.position;

    }
	
    
	void Update ()
    {
        updateDelay += Time.deltaTime;
        timeInSecs += Time.deltaTime;
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
            }
            distanceMeter += deltaDistance;

            distanceBar.value = (distanceMeter/fullDistance);
            //testbar.MoveSlider(distanceMeter / fullDistance);

            lastPlayerPosition = currentPlayerPosition;
            updateDelay = 0;
        }
        if (distanceMeter >= fullDistance && levelId > 0)
        {
            Debug.Log(timeInSecs);
            FinalLevelScore();
            stats.UnlockLevel(levelId + 1);
            Application.LoadLevel("EndLevel");
        }
	}

    void WriteToGuiPoints(int score)
    {
        pointsTextfield.text = score.ToString();
    }
    private void WriteToGuiBonus(int collected)
    {
        bonusTextField.text = collected.ToString() + " / " + maxBonusCount;
    }

    // Called from bonus object when triggered. Changes GUI text.
    public void AddScore(int newPoints)
    {
        stats.AddToCurrentPoints(newPoints);
        WriteToGuiPoints(stats.GetCurrentScore());
    }

    public void ContinueBonusStreak(bool gotMoreBonus)
    {
        if (gotMoreBonus)
        {
            stats.AddToBonusCount();
            WriteToGuiBonus(stats.GetCurrentBonus());
            if (longestStreak < bonusWithoutHit)
                longestStreak = bonusWithoutHit;
            ++bonusWithoutHit;
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
        Debug.Log("new: " + stats.GetCurrentScore());
        Debug.Log("highest: " + stats.GetlevelHighScore(levelId));
        // also saves best trophy
        int medal = stats.CompareToTrophyRequirements(levelId);

        /////////////////////////////////////////////////// for testing purposes
        switch (medal)
        {
            case 1:
                Debug.Log("You got gold!");
                break;
            case 2:
                Debug.Log("You got silver!");
                break;
            case 3:
                Debug.Log("You got bronze!");
                break;
            default:
                Debug.Log("No medal!");
                break;
        }
        //////////////////////////////////////////////////// end testing
        
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

    void CreateDistanceBar()
    {
        Vector3 pos = new Vector3(50, -328, 0);
        UI_c = FindObjectOfType<Canvas>();
        instantBar = Instantiate(distance, pos, Quaternion.identity) as GameObject;
        instantBar.transform.SetParent(UI_c.transform, false);
        instantBar.transform.localPosition = pos;

        distanceBar = instantBar.GetComponent<Slider>();
    }
}
