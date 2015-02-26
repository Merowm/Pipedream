using UnityEngine;
using System.Collections;

public class MusicVolumeReset : MonoBehaviour 
{
    // NB!!!!! Not tested yet with actual sound!

    // TODO: Set audio source to game music!
    
    public AudioSource music;
    public float startVolumeLevel;
    public bool hasCollectedItem;

    private float minVolumeLevel;
    private float timeFromCollect;
    private float dt;
    void Awake ()
    {
        startVolumeLevel = music.volume;
        minVolumeLevel = startVolumeLevel * 0.3f;
        hasCollectedItem = false;
        timeFromCollect = 0;
	}
	
    void FixedUpdate ()
    {
        dt = Time.deltaTime;
        timeFromCollect += dt;
	    if (hasCollectedItem == true && music.isPlaying)
        {
            if (music.volume < startVolumeLevel)
            {
                music.volume += 0.5f * dt;
            }
            else
            {
                music.volume = startVolumeLevel;
                hasCollectedItem = false;
                timeFromCollect = 0;
            }
        }
        // If no items are collected, music fades out slowly
        else if (music.volume > minVolumeLevel && timeFromCollect > 5)
        {
            music.volume -= 0.05f * dt;
        }
	}
}
