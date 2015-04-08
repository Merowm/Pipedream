﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Handles everything that should happen when player hits the bonus item.

public class CollectPoints : MonoBehaviour 
{
    public int itemScorePoints;
    // For debugging:
    public int bonusItemNumber;

    public Canvas levelUI;
    public LevelTimer timer;
    public AudioClip sound;

    VolControl volCtrl;
    
	void Awake ()
    {
        levelUI = FindObjectOfType<Canvas>();

        if (levelUI.tag != "gameLevelUI")
        {
            Debug.Log("Item found wrong UI!");
        }
        else
        {
            timer = GameObject.FindWithTag("levelTimer").GetComponent<LevelTimer>();
            volCtrl = FindObjectOfType<VolControl>();
        }
	}
	
	
	public void HitCollectable()
	{
        levelUI.GetComponent<FloatPointUI>().GeneratePoints(itemScorePoints);
		timer.AddScore(itemScorePoints);
        timer.ContinueBonusStreak(true);
        if (sound != null)
        {
            AudioSource.PlayClipAtPoint(sound, this.transform.position, volCtrl.effectVol);
        }
        Camera.main.GetComponent<MusicVolumeReset>().hasCollectedItem = true;
        //Destroy(this.transform.parent.gameObject);
        Debug.Log("collected bonus # " + bonusItemNumber);
        gameObject.SetActive(false);
    }
}
