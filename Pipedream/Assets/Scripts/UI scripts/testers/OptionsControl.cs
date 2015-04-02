using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsControl : MonoBehaviour {

    MusicVolumeReset musicCtrl;
    public Slider mainSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    VolControl globalVol;
    // Set button sound separately! (object exists!)
    
	void Start () 
    {
        musicCtrl = Camera.main.GetComponent<MusicVolumeReset>();
        globalVol = FindObjectOfType<VolControl>();    
	}

    public void SetMusicVolume()
    {
        musicCtrl.startVolumeLevel = musicSlider.value;
        globalVol.musicMaxVol = musicSlider.value;
    }
    public void SetSFXVolume()
    {
        globalVol.effectVol = sfxSlider.value;
        // button sound
    }
    public void SetMasterVolume()
    {
        AudioListener.volume = mainSlider.value;
    }
}
