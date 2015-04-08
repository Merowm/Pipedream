using UnityEngine;
using System.Collections;

public class MusicVolumeReset : MonoBehaviour 
{
    // NB!!!!! Not tested yet with actual sound!

    // TODO: Set audio source to game music!
    
    public AudioSource music;
    public float startVolumeLevel;
    public bool hasCollectedItem;
    VolControl globalVol;

    private float minVolumeLevel;
    private float timeFromCollect;
    private float dt;
    public float currentVol;
    void Awake ()
    {
        globalVol = FindObjectOfType<VolControl>();
        startVolumeLevel = globalVol.musicMaxVol;
        minVolumeLevel = startVolumeLevel * 0.3f;
        hasCollectedItem = false;
        timeFromCollect = 0;
        currentVol = startVolumeLevel;
	}
	
    void FixedUpdate ()
    {
        dt = Time.deltaTime;
        timeFromCollect += dt;
	    if (hasCollectedItem == true && music.isPlaying)
        {
            if (currentVol < startVolumeLevel)
            {
                currentVol += 0.5f * dt;
            }
            else
            {
                currentVol = startVolumeLevel;
                hasCollectedItem = false;
                timeFromCollect = 0;
            }
        }
        // If no items are collected, music fades out slowly
        else if (currentVol > minVolumeLevel && timeFromCollect > 5)
        {
            currentVol -= 0.05f * dt;
        }
        music.volume = currentVol;
	}
    public void ChangeMaxVolume(float volume)
    {
        startVolumeLevel = volume;
        minVolumeLevel = startVolumeLevel * 0.3f;
        currentVol = volume;
    }
}
