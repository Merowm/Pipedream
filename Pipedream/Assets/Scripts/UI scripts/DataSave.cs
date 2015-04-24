﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// TODO: Use PlayerPrefs anyway?
public class DataSave : MonoBehaviour
{
    string _name;
    Statistics stats;
    List<PlayerScores> scoreData;
    bool usesSlots = false; // TODO: change to true if save slot menu implemented! (or remove from code)

    [System.Serializable]
    public class PlayerScores
    {
        public int levelID;
        public int highScore;
        public int currentTrophy;
        public bool isUnlocked;
        // special goals
        public bool allCollected;
        public bool nothingHit;
        public bool bonusStreakDone;
        public bool specialFound;
               
        public PlayerScores(int id)
        {
            levelID = id;
        }
    }

    // call from save slot button if said button exists. Loads save file by name.
    // Otherwise called at start with default file name.
    public void UseSlot(string slotname)
    {
        _name = slotname;
        if (File.Exists(Application.persistentDataPath + "/" + _name + ".dat"))
        {
            BinaryFormatter bin = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + _name + ".dat", FileMode.Open);
            scoreData = (List<PlayerScores>)bin.Deserialize(file);
            file.Close();
            // send loaded data to statistics
            SetupStats();
            Debug.Log("data setup complete");
        }
        else
        {
            scoreData = new List<PlayerScores>();
            foreach (Statistics.levelData l in stats.levels)
            {
                scoreData.Add(new PlayerScores(l.levelID));
            }
            Debug.Log("new data setup");
            if (!stats.GetAvailability(1))
                stats.UnlockLevel(1);
        }
    }
    // clear slot (call from menu) Used only if slots are used.
    public void ClearSlot(string slotname)
    {
        File.Delete(Application.persistentDataPath + "/" + slotname + ".dat");
        SetupStats();
    }

	void Start () 
    {
        if (!(stats = GetComponent<Statistics>()))
            Debug.Log("no stats in saving system");
        if (!usesSlots)
        {
            UseSlot("defaultslot");          
        }
	}

    // Sets loaded player data to current game stats.
    public void SetupStats()
    {
        // Add new levels to save list if there are any
        if (!LevelListCheck())
        {
            LevelListUpdate();
        }
        // then load player scores to current game stats
        foreach (PlayerScores p in scoreData)
        {
            Statistics.levelData d = stats.FindLevel(p.levelID);
            d.currentTrophy = p.currentTrophy;
            d.highScore = p.highScore;
            d.isUnlocked = p.isUnlocked;
            d.allCollected = p.allCollected;
            d.nothingHit = p.nothingHit;
            d.bonusStreakDone = p.bonusStreakDone;
            d.specialFound = p.bonusStreakDone;
        }        
        // unlock first level! So game can be played.
        if (!stats.GetAvailability(1))
            stats.UnlockLevel(1);
    }

    // call this from wherever game has to save
    public void SendScore()
    {
        // TODO: set score data here in newscore:
        foreach (PlayerScores p in scoreData) // playerdata is changed
        {
            Statistics.levelData d = stats.FindLevel(p.levelID);
            p.currentTrophy = d.currentTrophy;
            p.highScore = d.highScore;
            p.isUnlocked = d.isUnlocked;
            p.allCollected = d.allCollected;
            p.nothingHit = d.nothingHit;
            p.bonusStreakDone = d.bonusStreakDone;
            p.specialFound = d.bonusStreakDone;
        }
        // then save to binary file.
        SaveData();
    }

    ////////////////////////////////
    /// Helpers.
    ////////////////////////////////

    void SaveData()
    {

        BinaryFormatter bin = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + _name + ".dat");
        bin.Serialize(file, scoreData);
        if (File.Exists(Application.persistentDataPath + "/" + _name + ".dat"))
        {
            Debug.Log("Data file made");
            Debug.Log(Application.persistentDataPath);
        }
        file.Close();
    }
    // in case new levels are added and old save files are old
    // creates new list entry with corresponding level ID
    private void LevelListUpdate()
    {        
        foreach (Statistics.levelData l in stats.levels)
        {
            if (FindScore(l.levelID) == null)
            {
                scoreData.Add(new PlayerScores(l.levelID));
            }
        }
    }
    private bool LevelListCheck()
    {
        if (stats.levels.Count > scoreData.Count)
        {
            return false;
        }
        return true;
    }
    private PlayerScores FindScore(int levelId)
    {
        foreach (PlayerScores ld in scoreData)
        {
            if (ld.levelID == levelId)
            {
                return ld;
            }
        }
        return null;
    }
}
