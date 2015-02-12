using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Handles everything that should happen when player hits the bonus item.

public class CollectPoints : MonoBehaviour 
{
    public int itemScorePoints;

    private Canvas levelUI;
	private CountScore score;
    private BonusCounter bonus;    
    
	void Awake ()
    {
        score = FindObjectOfType<CountScore>();
        bonus = FindObjectOfType<BonusCounter>();
        levelUI = FindObjectOfType<Canvas>();
        if (levelUI.tag != "gameLevelUI")
        {
            Debug.Log("Item found wrong UI!");
        }
	}
	
	
	public void HitCollectable()
	{
        levelUI.GetComponent<FloatPointUI>().GeneratePoints(itemScorePoints);
        bonus.UpdateBonusCount();
		score.AddScore(itemScorePoints);
        score.ContinueBonusStreak(true);
        //Camera.main.GetComponent<MusicVolumeReset>().hasCollectedItem = true;
        Destroy(this.gameObject);
    }
}
