using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsControl : MonoBehaviour {

    public Slider mainSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    VolControl globalVol;

    GameObject overlay;
    
	void Start () 
    {
        overlay = GameObject.Find("pauseScreen");
        globalVol = FindObjectOfType<VolControl>();
        musicSlider.value = globalVol.musicMaxVol;
        sfxSlider.value = globalVol.effectVol;
	}

    public void SetMusicVolume()
    {
        globalVol.SetMusicVolume(musicSlider.value);
    }
    public void SetSFXVolume()
    {
        globalVol.SetSFXVolume(sfxSlider.value);
    }
    public void SetMasterVolume()
    {
        AudioListener.volume = mainSlider.value;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        overlay.SetActive(false);    
    }
}
