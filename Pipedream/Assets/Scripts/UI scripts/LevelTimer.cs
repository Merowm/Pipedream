using UnityEngine;
using System.Collections;

public class LevelTimer : MonoBehaviour {

    public int levelId;
    public float distanceMeter;
    public float updateInterval;
    public Transform playerShip;
    public Texture2D distanceToGo;
    public Texture2D distanceGone;

    Statistics stats;
    EndLevelScore end;
    public int fullDistance;
     
    float deltaDistance;
    float updateDelay;
    Vector3 lastPlayerPosition;
    Vector3 currentPlayerPosition;
    Rect distanceIndicator;
    float gonePoint;

	
	void Awake ()
    {
        distanceMeter = 0;
        updateDelay = 0;
        distanceIndicator = new Rect(Screen.width / 2 - 200, 10, 400, 32);
        
	}
	void Start()
    {
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
            gonePoint = (distanceMeter/fullDistance) * 400;
            lastPlayerPosition = currentPlayerPosition;
            updateDelay = 0;
        }
        if (distanceMeter >= fullDistance)
        {
            end.LevelFinished();
        }
	}

    void OnGUI()
    {

        GUI.BeginGroup(distanceIndicator);
        {
            GUI.DrawTexture(new Rect(0, 0, 400, 32), distanceGone, ScaleMode.ScaleAndCrop);

            GUI.BeginGroup(new Rect(gonePoint, 0, 400, 32));
            {
                GUI.DrawTexture(new Rect(0, 0, 400 - gonePoint, 32), distanceToGo, ScaleMode.ScaleAndCrop);
            }
            GUI.EndGroup();
        }
        GUI.EndGroup();
    }

    // if needed to call from elsewhere.
    public void SaveDistanceAtJump(float distanceTravelledAtCurrentMode)
    {
        distanceMeter += distanceTravelledAtCurrentMode;
    }
}
