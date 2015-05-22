using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrentScore : MonoBehaviour {
 
    Statistics stats;
    Text pointScore;
    Text bonusScore;
    int level;
    DataSave saver;
    VolControl sound;

    GameObject[] medals;
    GameObject[] bonusObjectives;

	// Use this for initialization
	void Awake () 
    {

        GameObject s = GameObject.FindWithTag("statistics");
        stats = s.GetComponent<Statistics>();
        saver = s.GetComponent<DataSave>();
        sound = s.GetComponent<VolControl>();
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
        bonusObjectives[2] = GameObject.Find("getextra");
        bonusObjectives[3] = GameObject.Find("getmax");

        pointScore = GameObject.FindWithTag("Scoretext").GetComponent<Text>();
        bonusScore = GameObject.FindWithTag("Bonustext").GetComponent<Text>();
        level = stats.GetCurrentLevel();

    }

    void Start()
    {
        int maxBonus = stats.GetMaxBonusAmount(level);
        int maxScore = stats.GetLevelMaxpoints(level);
        pointScore.text = stats.GetCurrentScore().ToString();
        bonusScore.text = (stats.GetCurrentBonus() + " / " + maxBonus).ToString();
        SetMedal(stats.CompareToTrophyRequirements(level));


            if (Difficulty.currentDifficulty != Difficulty.DIFFICULTY.normal || stats.GetCurrentHitCount() != 0)
                UnCheckGoal(bonusObjectives[0]);

            if (Difficulty.currentDifficulty != Difficulty.DIFFICULTY.normal || stats.GetCurrentBonus() != maxBonus)
                UnCheckGoal(bonusObjectives[1]);

            if (Difficulty.currentDifficulty != Difficulty.DIFFICULTY.normal || !stats.GetSpecialAcquired())
                UnCheckGoal(bonusObjectives[2]);

            if (Difficulty.currentDifficulty != Difficulty.DIFFICULTY.normal || stats.GetCurrentScore() != maxScore)
                UnCheckGoal(bonusObjectives[3]);

            sound.PlayVictorySound();
        //saver.SendScore(level);
    }

	
    void SetMedal(int trophy)
    {
        if (trophy > 0)
        medals[trophy - 1].SetActive(true);
    }
    void UnCheckGoal(GameObject yesno)
    {
        Transform yes = yesno.transform.Find("yes");
        yes.gameObject.SetActive(false);
         
    }

    public void Reset()
    {

        stats.ResetScore();
    }

}
