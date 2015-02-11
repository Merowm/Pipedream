using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BonusCounter : MonoBehaviour {

    int levelId;

    int maxBonusCount;
    GameObject statObj;
    Statistics stats;
    LevelTimer levelControl;
    Text text;    

	// Use this for initialization
	void Start ()
    {
        levelControl = FindObjectOfType<LevelTimer>();
        statObj = GameObject.FindGameObjectWithTag("statistics");
        if (statObj != null)
            stats = statObj.GetComponent<Statistics>();
        levelId = levelControl.GetCurrentLevel();
        maxBonusCount = stats.GetMaxBonusAmount(levelId);
        text = GameObject.FindWithTag("Bonustext").GetComponent<Text>();
        WriteToGui(0);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    // TODO: performance check - if very heavy, use local variable
    public void UpdateBonusCount()
    {
        stats.AddToBonusCount();
        WriteToGui(stats.GetCurrentBonus());
    }

    private void WriteToGui(int collected)
    {
        text.text = collected.ToString() + " / " + maxBonusCount;
    }
}
