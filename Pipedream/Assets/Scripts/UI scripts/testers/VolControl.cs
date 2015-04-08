using UnityEngine;
using System.Collections;

public class VolControl : MonoBehaviour {

    public float musicMaxVol;
    public float effectVol;
    public float masterVol;

    public AudioClip whooshSound;
    public AudioClip buttonEffect;
    public AudioClip bonusEffect;
    public AudioClip crashEffect;
    // set from each scene? NB smooth transitions!
    public AudioSource music;

    // vars for collectible fade/reset
    public bool hasCollectedItem;
    float timeFromCollect;
    float dt;
    public float fadeRate;
    
	// Use this for initialization
	void Start ()
    {
        music = Camera.main.GetComponent<AudioSource>();
        AudioListener.volume = masterVol;        
        fadeRate = 1;
	}
	
	// Update is called once per frame
	void Update () {

	}
    public void SetMusicVolume(float vol)
    {        
        musicMaxVol = vol;
        music.volume = musicMaxVol * fadeRate;
    }
    public void SetSFXVolume(float vol)
    {
        effectVol = vol;

        Time.timeScale = 1.0f;
        AudioSource.PlayClipAtPoint(buttonEffect, Input.mousePosition, effectVol);
        Time.timeScale = 0.0f;
    }
    public void SetMasterVolume(float vol)
    {
        masterVol = vol;
        AudioListener.volume = masterVol;
    }

    // updating collectible fade/reset
    void FixedUpdate()
    {
        if (!music)
            music = Camera.main.GetComponent<AudioSource>();
        dt = Time.deltaTime;
        timeFromCollect += dt;
        //Debug.Log(hasCollectedItem);
        if (hasCollectedItem == true && music.isPlaying)
        {                Debug.Log("music reset!");
            if (fadeRate < 1)
            {
                fadeRate += 0.5f * dt;
            }
            else
            {
                fadeRate = 1;
                hasCollectedItem = false;
                timeFromCollect = 0;

            }
        }
        // If no items are collected, music fades out slowly
        else if (fadeRate > 0.3f && timeFromCollect > 5)
        {
            fadeRate -= 0.05f * dt;
        }
        music.volume = musicMaxVol * fadeRate;
    }
    public void PlayButtonEffect()
    {
        AudioSource.PlayClipAtPoint(buttonEffect, Input.mousePosition, effectVol);
    }
    public void TestCollectSound()
    {
        AudioSource.PlayClipAtPoint(bonusEffect, Input.mousePosition, effectVol);
        hasCollectedItem = true;
    }
}
