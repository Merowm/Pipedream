using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Handles everything that should happen when player hits the bonus item.

public class CollectPoints : MonoBehaviour 
{
    public int itemScorePoints;
    public bool thisIsSpecial;
    // For debugging:
    public int bonusItemNumber;

    public Canvas levelUI;
    public LevelTimer timer;

    AudioClip sound;
    VolControl volCtrl;
    
	void Awake ()
    {
        levelUI = FindObjectOfType<Canvas>();

        Canvas[] all = FindObjectsOfType<Canvas>();
        foreach (Canvas c in all)
        {
            if (c.tag == "gameLevelUI")
                levelUI = c;
        }
        if (GameObject.FindWithTag("levelTimer") != null)
        {
            timer = GameObject.FindWithTag("levelTimer").GetComponent<LevelTimer>();
        }
        if (volCtrl = FindObjectOfType<VolControl>())
        {
            sound = volCtrl.bonusEffect;
        }
	}
	
    void Start ()
    {
        itemScorePoints = (int)(itemScorePoints * MovementForward.difficultyMultiplier);
    }
	
	public void HitCollectable()
	{
        levelUI.GetComponent<FloatPointUI>().GeneratePoints(itemScorePoints);
		timer.AddScore(itemScorePoints);
        timer.ContinueBonusStreak(true);
        if (thisIsSpecial)
        {
            timer.GotSpecial();
        }
        if (sound != null)
        {
            AudioSource.PlayClipAtPoint(sound, this.transform.position, volCtrl.effectVol);
        }
        volCtrl.hasCollectedItem = true;
        //Destroy(this.transform.parent.gameObject);
        Debug.Log("collected bonus # " + bonusItemNumber);
        gameObject.SetActive(false);
    }
}
