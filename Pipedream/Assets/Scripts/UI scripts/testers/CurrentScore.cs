using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrentScore : MonoBehaviour {

    Statistics stats;
    Text pointScore;
    Text bonusScore;
    int level;

    GameObject[] medals;
    GameObject[] bonusObjectives;

	// Use this for initialization
	void Awake () 
    {
        stats = GameObject.FindWithTag("statistics").GetComponent<Statistics>();
        medals = new GameObject[3];
        medals[0] = GameObject.Find("gold");
        medals[1] = GameObject.Find("silver");
        medals[2] = GameObject.Find("bronze");
        foreach (GameObject g in medals)
        {
            g.SetActive(false);
        }
        bonusObjectives = new GameObject[4];
        bonusObjectives[0] = GameObject.Find("nohit");
        bonusObjectives[1] = GameObject.Find("getall");
        bonusObjectives[2] = GameObject.Find("getstreak");
        bonusObjectives[3] = GameObject.Find("getextra");

        pointScore = GameObject.FindWithTag("Scoretext").GetComponent<Text>();
        bonusScore = GameObject.FindWithTag("Bonustext").GetComponent<Text>();
        level = stats.GetCurrentLevel();

    }

    void Start()
    {
        int maxBonus = stats.GetMaxBonusAmount(level);
        pointScore.text = stats.GetCurrentScore().ToString();
        bonusScore.text = (stats.GetCurrentBonus() + " / " + maxBonus).ToString();
        SetMedal(stats.CompareToTrophyRequirements(level));
        CheckGoal(bonusObjectives[0], stats.GetCurrentHitCount() == 0);
        CheckGoal(bonusObjectives[1], stats.GetCurrentBonus() == maxBonus);
        // bool values set for checking only!
        // TODO: replace with valid data!
        CheckGoal(bonusObjectives[2], stats.GetLongestStreak() >= maxBonus * 0.5f); ////////////// Placeholder value!
        CheckGoal(bonusObjectives[3], stats.GetSpecialAcquired());

    }
	
    void SetMedal(int trophy)
    {
        if (trophy > 0)
        medals[trophy - 1].SetActive(true);
    }
    void CheckGoal(GameObject yesno, bool gotIt)
    {
        if (gotIt)
            yesno.GetComponent<CanvasRenderer>().SetColor(Color.green);
        else
            yesno.GetComponent<CanvasRenderer>().SetColor(Color.red);
    }

    public void Reset()
    {
        stats.ResetScore();
    }

}
