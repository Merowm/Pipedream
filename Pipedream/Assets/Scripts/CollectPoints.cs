using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Handles everything that should happen when player hits the bonus item.

public class CollectPoints : MonoBehaviour 
{
    public int itemScorePoints;
    public Canvas floatingPoints;


	private CountScore score;
    private BonusCounter bonus;
    private Canvas points;
    
	void Awake ()
    {
        score = FindObjectOfType<CountScore>();
        bonus = FindObjectOfType<BonusCounter>();
	}
	
	
	public void Collect()
	{
        points = Instantiate(floatingPoints, this.transform.position, Quaternion.identity) as Canvas;
        points.GetComponent<Text>().text = itemScorePoints.ToString();
        bonus.UpdateBonusCount();
		score.AddScore(itemScorePoints);
        score.ContinueBonusStreak(true);
        //Camera.main.GetComponent<MusicVolumeReset>().hasCollectedItem = true;
    }
}
