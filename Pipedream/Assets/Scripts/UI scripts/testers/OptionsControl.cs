using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsControl : MonoBehaviour {

    public Slider mainSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle muteBox;
    VolControl globalVol;

    GameObject overlay;
    
	void Start () 
    {
        overlay = GameObject.Find("pauseScreen");
        globalVol = FindObjectOfType<VolControl>();
        musicSlider.value = globalVol.musicMaxVol;
        sfxSlider.value = globalVol.effectVol;
        mainSlider.value = globalVol.masterVol;
        muteBox.isOn = globalVol.isMute;
	}

    public void SetMusicVolume()
    {
        globalVol.SetMusicVolume(musicSlider.value);
    }
    public void SetSFXVolume()
    {
        globalVol.SetSFXVolume(sfxSlider.value);
    }
    public void PlaySoundEffect()
    {
        globalVol.PlayButtonEffect();
    }
    public void SetMasterVolume()
    {
        globalVol.SetMasterVolume(mainSlider.value);
        muteBox.isOn = false;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        overlay.SetActive(false);    
    }
    public void MuteAll()
    {
        globalVol.MuteAudio(muteBox.isOn);
    }
}
