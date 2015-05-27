using UnityEngine;
using System.Collections;

public class VolControl : MonoBehaviour {

    public float musicMaxVol;
    public float effectVol;
    public float masterVol;
    public bool isMute;
    public bool tutorialIsOn;

    public AudioClip whooshSound;
    public AudioSource buttonEffect;
    public AudioClip bonusEffect;
    public AudioClip crashEffect;
    public AudioClip victorySound;
    public AudioSource countdown;
    // set from each scene? NB smooth transitions!
    public AudioSource music;
    public AudioClip[] jukebox;
    
    // playlist index is level number (0 for menu)
    // TODO: Change to interactive list?
    public int[] playlist;
    public bool isInMenu;

    // vars for collectible fade/reset
    public bool hasCollectedItem;
    float timeFromCollect;
    float dt;
    float fadeRate;

    DataSave saver;
    // var for saving last value of masterVol when muted
    float lastMaster;

    // smooth changing of tracks
    bool fadingOut = false;
    bool fadingIn = false;
    int currentTrack = 0;

    void Awake ()
    {
        saver = GetComponent<DataSave>();
    }
	void Start ()
    {
        
        fadeRate = 1;        
        music.clip = jukebox[currentTrack];
        music.volume = musicMaxVol * masterVol * fadeRate;
        countdown.volume = effectVol * masterVol;
        music.Play();
	}

    public void SetMusicVolume(float vol)
    {        
        musicMaxVol = vol;
        music.volume = musicMaxVol * fadeRate * masterVol;
        saver.SetVolume(); 
    }
    public void SetSFXVolume(float vol)
    {
        effectVol = vol;
        saver.SetVolume(); 
    }
    public void SetMasterVolume(float vol)
    {
        masterVol = vol;
        music.volume = musicMaxVol * masterVol * fadeRate;  
        saver.SetVolume();      
    }
    public void MuteAudio(bool muted)
    {
        isMute = muted;
        if (muted)
        {
            lastMaster = masterVol;
            masterVol = 0;
        }
        else masterVol = lastMaster;
        music.volume = musicMaxVol * masterVol * fadeRate;
    }
    public void TutorialYesNo(bool yesno)
    {
        tutorialIsOn = yesno;
    }
    // updating collectible fade/reset
    void FixedUpdate()
    {
        if (fadingOut)
        {
            if (masterVol > 0)
                masterVol -= Time.deltaTime * 2;
            else
            {
                StartNextTrack();
                fadingOut = false;
                fadingIn = true;
            }
        }
        if (fadingIn)
        {
            if (masterVol < lastMaster)
                masterVol += Time.deltaTime * 2;
            else
            {
                masterVol = lastMaster;
                fadingIn = false;
            }
        }
        // //////// Code for music fading if items not collected
        //if (!isInMenu)
        //{
        //    dt = Time.deltaTime;
        //    timeFromCollect += dt;
            
        //    if (hasCollectedItem == true && music.isPlaying)
        //    {
        //        Debug.Log("music reset!");
        //        if (fadeRate < 1)
        //        {
        //            fadeRate += 0.5f * dt;
        //        }
        //        else
        //        {
        //            fadeRate = 1;
        //            hasCollectedItem = false;
        //            timeFromCollect = 0;
        //        }
        //    }
        //    // If no items are collected, music fades out slowly
        //    else if (fadeRate > 0.3f && timeFromCollect > 5)
        //    {
        //        fadeRate -= 0.05f * dt;
        //    }           
        //}
        //if (music)
        //    music.volume = musicMaxVol * fadeRate * masterVol;
    }
    public void PlayButtonEffect()
    {
        buttonEffect.volume = effectVol * masterVol;
        buttonEffect.Play();
    }
    public void PlayCollectSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(bonusEffect, position, effectVol * masterVol);
    }
    public void PlayVictorySound()
    {
        AudioSource.PlayClipAtPoint(victorySound, new Vector3(0, 1, -10), effectVol * masterVol);
    }
    public void SetMusicType(bool isMenu, int level)
    {
        if (currentTrack != level)
        {
            currentTrack = level;
            Debug.Log("changing music...");
            fadingOut = true;
            lastMaster = masterVol; // remember vol settings
        }
        isInMenu = isMenu;
    }

    // sound effect tester methods
    public void TestCollectSound()
    {
        AudioSource.PlayClipAtPoint(bonusEffect, Input.mousePosition, effectVol * masterVol);
        //hasCollectedItem = true;
    }
    public void TestCrashEffect(Vector3 position)
    {
        // To be implemented if/when we have crash effect.
        AudioSource.PlayClipAtPoint(crashEffect, position, effectVol * masterVol);
    }
    public void CountDown()
    {
        if (!isInMenu)
        {
            countdown.volume = effectVol * masterVol;
            countdown.Play();
        }
    }
    void StartNextTrack()
    {
        int track;
        if (currentTrack == 99)
            track = (Random.Range(0, 100) % 2) + 1;
        else track = playlist[currentTrack];
        music.clip = jukebox[track];
        music.Play();        
    }
}
