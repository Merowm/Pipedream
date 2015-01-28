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

    void Awake ()
    {
        startVolumeLevel = music.volume;
        minVolumeLevel = startVolumeLevel * 0.3f;
        hasCollectedItem = false;
	}
	
    void FixedUpdate ()
    {
	    if (hasCollectedItem == true && music.isPlaying)
        {
            if (music.volume < startVolumeLevel)
            {
                music.volume += 0.7f * Time.deltaTime;
            }
            else
            {
                music.volume = startVolumeLevel;
                hasCollectedItem = false;
            }
        }
        // If no items are collected, music fades out slowly
        else if (music.volume > minVolumeLevel)
        {
            music.volume -= 0.05f * Time.deltaTime;
        }
	}
}
