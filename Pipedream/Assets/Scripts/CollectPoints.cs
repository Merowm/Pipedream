using UnityEngine;
using System.Collections;

// Handles everything that should happen when player hits the bonus item.

public class CollectPoints : MonoBehaviour 
{
    public int itemScorePoints;
    public Transform floatingPoints;


	private CountScore score;
    private BonusCounter bonus;
    
	void Awake ()
    {
        score = FindObjectOfType<CountScore>();
        bonus = FindObjectOfType<BonusCounter>();
	}
	
	
	void OnTriggerEnter(Collider other)
	{
        Debug.Log("Hit trigger");
        Instantiate(floatingPoints, this.transform.position, Quaternion.identity);
        bonus.UpdateBonusCount();
		score.AddScore(itemScorePoints);
        score.ContinueBonusStreak(true);
        //Camera.main.GetComponent<MusicVolumeReset>().hasCollectedItem = true;
        Destroy(this.gameObject);
    }
}
