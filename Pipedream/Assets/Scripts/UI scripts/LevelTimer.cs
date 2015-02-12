﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelTimer : MonoBehaviour {

    // Leave at 0 when testing
    public int levelId;
    public float distanceMeter;
    public float updateInterval;
    public Transform playerShip;
    public float timeInSecs;

    Statistics stats;
    EndLevelScore end;
    Slider distanceBar;
    public int fullDistance;
     
    float deltaDistance;
    float updateDelay;
    Vector3 lastPlayerPosition;
    Vector3 currentPlayerPosition;

    int bonusWithoutHit;
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
            fullDistance = 10000000;
            maxBonusCount = 100;
            Debug.Log("Timer in testing mode");
        }

        
        playerShip = GameObject.FindWithTag("Player").GetComponent<Transform>();
        if (playerShip != null)
        {
            Debug.Log("Timer found player by tag");
        }

        distanceBar = GameObject.FindWithTag("travelIndicator").GetComponent<Slider>();
        if (distanceBar != null)
        {
            Debug.Log("Timer found distanceBar by tag");
        }

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

            lastPlayerPosition = currentPlayerPosition;
            updateDelay = 0;
        }
        if (distanceMeter >= fullDistance && levelId > 0)
        {
            Debug.Log(timeInSecs);
            FinalLevelScore();
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
            ++bonusWithoutHit;
        }
        else
        {
            stats.AddToHitCount();
            bonusWithoutHit = 0;
        }
    }

    // Called when level ends
    public void FinalLevelScore()
    {
        stats.SetFinalLevelScore(levelId);
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
        stats.ResetScore();
    }

    // if needed to call from elsewhere.
    public void SaveDistanceAtJump(float distanceTravelledAtCurrentMode)
    {
        distanceMeter += distanceTravelledAtCurrentMode;
    }

    // Called from other scorekeeping objects.
    public int GetCurrentLevel()
    {
        return levelId;
    }


}
