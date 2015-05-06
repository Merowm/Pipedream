﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;

// TODO: Use PlayerPrefs anyway?
public class DataSave : MonoBehaviour
{
    string _name;
    Statistics stats;

    // PlayerPerfs: data saved as key/value pairs
    // only float and int type values accepted (and string; may cause errors though)
    // keys: level id + stat name e.g. "2highScore"
	void Start () 
    {
        if (!(stats = GetComponent<Statistics>()))
            Debug.Log("no stats in saving system");
        LoadSettings();
        LoadScores();
	}
    // clear save (called from OptionsControl)
    public void ClearSlot()
    {
        PlayerPrefs.DeleteAll();
        // set in-game data to default
        stats.ResetGame();
        // save custom settings
        SetSettings();
    }

    // call this from wherever game has to save
    public void SendScore(int level)
    {
        SaveLevelScore(level);
    }

    ////////////////////////////////
    /// Helpers.
    ////////////////////////////////

    // With PlayerPrefs. Default value (if key not found) should be 0 (treated as false).
    private void LoadScores()
    {
        foreach (Statistics.levelData l in stats.levels)
        {   
            int id = l.levelID;    
            l.highScore = GetSavedStatInt(id, "highScore");
            l.currentTrophy = GetSavedStatInt(id, "currentTrophy");
            l.isUnlocked = GetSavedStatBool(id, "isUnlocked");
            // special goals
            l.allCollected = GetSavedStatBool(id, "allCollected");
            l.nothingHit = GetSavedStatBool(id, "nothingHit");
            l.bonusStreakDone = GetSavedStatBool(id, "bonusStreakDone");
            l.specialFound = GetSavedStatBool(id, "specialFound");
        }
        // get general settings data
    }
    private void LoadSettings()
    {
        string colname;
        for (int i = 0; i < 8; i++)
        {
           colname = "color" + i.ToString();
           stats.colors[i].r = (byte)PlayerPrefs.GetInt(colname + "R");
           stats.colors[i].b = (byte)PlayerPrefs.GetInt(colname + "B");
           stats.colors[i].g = (byte)PlayerPrefs.GetInt(colname + "G");
           stats.colors[i].a = (byte)PlayerPrefs.GetInt(colname + "A");
        }
        stats.difficulty = PlayerPrefs.GetInt("difficulty");
    }      
    // save general settings
    public void SetSettings()
    {
        string colname;
        for (int i = 0; i < 8; i++)
        {   
            colname = "color" + i.ToString();
            PlayerPrefs.SetInt(colname + "R", stats.colors[i].r);
            PlayerPrefs.SetInt(colname + "B", stats.colors[i].b);
            PlayerPrefs.SetInt(colname + "G", stats.colors[i].g);
            PlayerPrefs.SetInt(colname + "A", stats.colors[i].a);
        }
        PlayerPrefs.SetInt("difficulty", stats.difficulty);
        PlayerPrefs.Save();
    }
    // save all game data. Is this needed at all
    private void SaveScores()
    {
        foreach (Statistics.levelData d in stats.levels)
        {
            int id = d.levelID;
            SaveLevelScore(id);
        }
        // general settings data 
        SetSettings();
    }
    // change saved data for one level (called at end of level)
    private void SaveLevelScore(int id)
    {
        Statistics.levelData d = stats.FindLevel(id);
        SetSavedStatInt(id, "highScore", d.highScore);
        SetSavedStatInt(id, "currentTrophy", d.currentTrophy);
        SetSavedStatBool(id, "isUnlocked", d.isUnlocked);
        // special goals
        SetSavedStatBool(id, "allCollected", d.allCollected);
        SetSavedStatBool(id, "nothingHit", d.nothingHit);
        SetSavedStatBool(id, "bonusStreakDone", d.bonusStreakDone);
        SetSavedStatBool(id, "specialFound", d.specialFound);
        PlayerPrefs.Save();
    }
    private int GetSavedStatInt(int level, string stat)
    {
        string statname = level.ToString() + stat;
        return PlayerPrefs.GetInt(statname);        
    }
    private bool GetSavedStatBool(int level, string stat)
    {
        string statname = level.ToString() + stat;
        return (PlayerPrefs.GetInt(statname) != 0);
    }
    private void SetSavedStatInt(int level, string stat, int val)
    {
        string statname = level.ToString() + stat;
        PlayerPrefs.SetInt(statname, val);
    }
    private void SetSavedStatBool(int level, string stat, bool val)
    {
        string statname = level.ToString() + stat;        
        if (val)
            PlayerPrefs.SetInt(statname, 1);
        else PlayerPrefs.SetInt(statname, 0);        
    }
}
