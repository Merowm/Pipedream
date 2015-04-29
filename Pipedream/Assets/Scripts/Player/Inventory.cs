using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public List<string> itemNames = new List<string>();
    public List<bool> items = new List<bool>();
    public bool timeSlowed = false;
    public float slowDownDuration = 5.0f;
    public float slowDownAmount = 0.5f;

    private Health health;
    private float timer = 0.0f;

	void Awake ()
    {
        health = transform.GetComponent<Health>();
	}

    void Update ()
    {
        //If time is slowed
        if (timeSlowed)
        {
            //Slow down time
            Time.timeScale = slowDownAmount;
            //Update timer with compensation for the slow down
            timer += Time.deltaTime * (1.0f / slowDownAmount);
            //When timer finishes
            if (timer >= slowDownDuration)
            {
                //Reset time to normal
                Time.timeScale = 1.0f;
                timeSlowed = false;
                //Reset timer
                timer = 0.0f;
            }
            //Apply slow down effect to all audio aswell
            AudioSource[] audioSources = Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            for (int i = 0; i < audioSources.Length; ++i)
            {
                audioSources [i].pitch = Time.timeScale;
            }
        }
    }

	public void UseItem (int index)
    {
        if (items[index])
        {
            if (index == 0)
            {
                health.Repair();
            }
            else if (index == 1)
            {
                health.SetInvulnerability();
            }
            else if (index == 2)
            {
                timeSlowed = true;
            }
            items[index] = false;
        }
	}
}
