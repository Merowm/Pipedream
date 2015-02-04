using UnityEngine;
using System.Collections;

public class LevelTimer : MonoBehaviour {

    public float timeTimer;

    GameObject statObj;
    Statistics stats;
	// Use this for initialization
	void Awake ()
    {
        timeTimer = 0;
	}
	void Start()
    {
        statObj = GameObject.FindGameObjectWithTag("statistics");
        if (statObj != null)
            stats = statObj.GetComponent<Statistics>();
    }
	// Update is called once per frame
	void Update ()
    {
        timeTimer += Time.deltaTime;
        guiText.text = ((int)timeTimer).ToString();
	}

    public void FinalTimeBonus()
    {
        // Send final time to CountScore / Statistics?
       Debug.Log(stats.CountTimeBonus(timeTimer));
    }
}
