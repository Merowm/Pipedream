using UnityEngine;
using System.Collections;

public class BonusCounter : MonoBehaviour {

    public int levelId;

    int maxBonusCount;
    GameObject statObj;
    Statistics stats;

	// Use this for initialization
	void Start ()
    {
        statObj = GameObject.FindGameObjectWithTag("statistics");
        if (statObj != null)
            stats = statObj.GetComponent<Statistics>();
        maxBonusCount = stats.GetMaxBonusAmount(levelId);
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
        guiText.text = collected.ToString() + " / " + maxBonusCount;
    }
}
