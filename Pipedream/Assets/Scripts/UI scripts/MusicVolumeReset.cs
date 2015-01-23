using UnityEngine;
using System.Collections;

public class MusicVolumeReset : MonoBehaviour {

    // TODO: Script for bonus item to raise music volume level back to original
    // Use smooth transition!

    public AudioSource music;
    public float startVolumeLevel;

    void Awake ()
    {
        startVolumeLevel = music.volume;
	}
	
    // TODO: Activate when player collides with bonus item (called from item?)
	void FixedUpdate ()
    {
	    if (music.isPlaying)
        {
            if (music.volume < startVolumeLevel)
            {
                music.volume += Mathf.Lerp(music.volume, startVolumeLevel, 0.3f) * Time.deltaTime;
            }
            else
            {
                music.volume = startVolumeLevel;
                this.enabled = false;
            }
        }
	}
}
