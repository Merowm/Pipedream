using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelTimer : MonoBehaviour {

    public int levelId;
    public float distanceMeter;
    public float updateInterval;
    public Transform playerShip;

    Statistics stats;
    EndLevelScore end;
    Slider distanceBar;
    public int fullDistance;
     
    float deltaDistance;
    float updateDelay;
    Vector3 lastPlayerPosition;
    Vector3 currentPlayerPosition;


	
	void Awake ()
    {
        distanceMeter = 0;
        updateDelay = 0;

        
	}
	void Start()
    {
        distanceBar = GameObject.FindWithTag("travelIndicator").GetComponent<Slider>();
        end = GetComponent<EndLevelScore>();
        stats = FindObjectOfType<Statistics>();
        fullDistance = stats.GetLevelDistance(levelId);
        lastPlayerPosition = playerShip.position;
        stats.SetLevelPlayed(levelId);
    }
	
    
	void Update ()
    {
        updateDelay += Time.deltaTime;
        // TODO: Change to use percentage of full distance? (why?)
        if (updateDelay >= updateInterval)
        {
            currentPlayerPosition = playerShip.position;
            deltaDistance = currentPlayerPosition.z - lastPlayerPosition.z;
            // save travelled distance at jump
            if (deltaDistance < 0)
            {
                distanceMeter += lastPlayerPosition.z;
            }
            distanceMeter += deltaDistance;

            distanceBar.value = (distanceMeter/fullDistance);

            lastPlayerPosition = currentPlayerPosition;
            updateDelay = 0;
        }
        if (distanceMeter >= fullDistance)
        {
            end.LevelFinished();
        }
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
