using UnityEngine;
using System.Collections;

public class VolControl : MonoBehaviour {

    public float musicMaxVol;
    public float effectVol;
    public float masterVol;
    public bool isMute;

    public AudioClip whooshSound;
    public AudioSource buttonEffect;
    public AudioClip bonusEffect;
    public AudioClip crashEffect;
    // set from each scene? NB smooth transitions!
    public AudioSource music;
    public AudioSource menuMusic;
    public bool isInMenu;

    // vars for collectible fade/reset
    public bool hasCollectedItem;
    float timeFromCollect;
    float dt;
    float fadeRate;
    
	// Use this for initialization
	void Start ()
    {
        AudioListener.volume = masterVol;
        fadeRate = 1;
        isInMenu = true;
	}
	
	// Update is called once per frame
	void Update () {

	}
    public void SetMusicVolume(float vol)
    {        
        musicMaxVol = vol;
        if (isInMenu)
            menuMusic.volume = musicMaxVol;
        else if (music)
        {
            music.volume = musicMaxVol * fadeRate;
        }
    }
    public void SetSFXVolume(float vol)
    {
        effectVol = vol;
    }
    public void SetMasterVolume(float vol)
    {
        masterVol = vol;
        AudioListener.volume = masterVol;
    }
    public void MuteAudio(bool muted)
    {
        isMute = muted;
        if (muted)
        {
            AudioListener.volume = 0;
        }
        else AudioListener.volume = masterVol;
    }

    // updating collectible fade/reset
    void FixedUpdate()
    {
        if (!isInMenu)
        {
            dt = Time.deltaTime;
            timeFromCollect += dt;
            
            if (hasCollectedItem == true && music.isPlaying)
            {
                Debug.Log("music reset!");
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
            if (music)
            music.volume = musicMaxVol * fadeRate;
        }
        
    }
    public void PlayButtonEffect()
    {
        buttonEffect.volume = effectVol;
        buttonEffect.Play();
    }
    public void SetMusicType(bool isMenu)
    {
        if (isMenu)
        {
            isInMenu = true;
            if (music)
                music.enabled = false;
            if (!menuMusic.enabled)
                menuMusic.enabled = true;
            menuMusic.volume = musicMaxVol;
        }
        else
        {
            isInMenu = false;
            music = Camera.main.GetComponent<AudioSource>();
            if (music)
                music.enabled = true;
            menuMusic.enabled = false;
        }
    }

    // sound effect tester methods
    public void TestCollectSound()
    {
        AudioSource.PlayClipAtPoint(bonusEffect, Input.mousePosition, effectVol);
        hasCollectedItem = true;
    }
    public void TestCrashEffect()
    {

    }
}
