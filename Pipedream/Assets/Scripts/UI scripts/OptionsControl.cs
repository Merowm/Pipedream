using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsControl : MonoBehaviour {

    public Slider mainSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle muteBox;
    public Toggle tutorialBox;
    VolControl globalVol;

    GameObject overlay;
    Health hpGUI;
    
	void Start () 
    {
        overlay = GameObject.Find("pauseScreen");
        globalVol = FindObjectOfType<VolControl>();
        if (hpGUI = FindObjectOfType<Health>())
            Debug.Log("pause control found health");
        musicSlider.value = globalVol.musicMaxVol;
        sfxSlider.value = globalVol.effectVol;
        mainSlider.value = globalVol.masterVol;
        muteBox.isOn = globalVol.isMute;
        if (globalVol.isInMenu)
            tutorialBox.isOn = globalVol.tutorialIsOn;
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
        if (hpGUI != null)
        {
            if (hpGUI.IsAlive())
            {
                Time.timeScale = 1;
                overlay.SetActive(false);
            }
        }
        else
        {
            Time.timeScale = 1;
            overlay.SetActive(false);
        }
    }
    public void MuteAll()
    {
        globalVol.MuteAudio(muteBox.isOn);
    }
    public void ToggleTutorial()
    {
        globalVol.TutorialYesNo(tutorialBox.isOn);
    }
}
