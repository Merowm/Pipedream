﻿using UnityEngine;
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
        if (!(overlay = GameObject.Find("pauseScreen")))
            overlay = GameObject.Find("volumes");
        if (globalVol = FindObjectOfType<VolControl>()) { }
        else Debug.Log("volume control not found!");
        if (hpGUI = FindObjectOfType<Health>())
            Debug.Log("pause control found health");
        musicSlider.value = globalVol.musicMaxVol;
        sfxSlider.value = globalVol.effectVol;
        mainSlider.value = globalVol.masterVol;
        muteBox.isOn = globalVol.isMute;
        Debug.Log(overlay + ": Stats thinks we are in menu: " + globalVol.isInMenu);
        if (globalVol.isInMenu && tutorialBox)
            tutorialBox.isOn = globalVol.tutorialIsOn;

        overlay.SetActive(false);
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
        if (globalVol)
            globalVol.PlayButtonEffect();
        else 
        {
            Debug.Log("volumeControl missing");
        }
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
