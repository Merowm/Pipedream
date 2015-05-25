using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DataSave : MonoBehaviour
{
    string _name;
    Statistics stats;
    VolControl vol;

    // PlayerPerfs: data saved as key/value pairs
    // only float and int type values accepted (and string; may cause errors though)
    // keys: username + level id + stat name e.g. "Guest2highScore"
    // Use StatName() to form key name
    // default name if not connected: "player"
    void Awake()
    {
        if (!(stats = GetComponent<Statistics>()))
            Debug.Log("no stats in saving system");
        if (!(vol = GetComponent<VolControl>()))
            Debug.Log("no volControl in saving system");
    }
	void Start ()
    {
        LoadSettings();
        LoadScores();
        LoadKongStats();
        stats.UnlockLevel(1);
	}
    // clear save (called from OptionsControl)
    public void ClearSlot()
    {
        // Only clear out current user's game progress data!
        clearthings();        
        // set in-game data to default
        stats.ResetGame();
        // save custom settings
        SetSettings();
    }
    private void clearthings()
    {        
        for (int i = 1; i < stats.levels.Count + 1; ++i)
        {
            PlayerPrefs.DeleteKey(StatName("highScore", i));
            PlayerPrefs.DeleteKey(StatName("currentTrophy", i));
            PlayerPrefs.DeleteKey(StatName("isUnlocked", i));
            PlayerPrefs.DeleteKey(StatName("allCollected", i));
            PlayerPrefs.DeleteKey(StatName("nothingHit", i));
            PlayerPrefs.DeleteKey(StatName("bonusStreakDone", i));
            PlayerPrefs.DeleteKey(StatName("specialFound", i));
            PlayerPrefs.DeleteKey(StatName("fullPoints", i));
        }
        PlayerPrefs.DeleteKey(StatName("endlessTime"));
        PlayerPrefs.DeleteKey(StatName("endlessScore"));
        PlayerPrefs.DeleteKey(StatName("endlessItems"));
        PlayerPrefs.DeleteKey(StatName("endlessSurvivalTimeOnNormal"));
        PlayerPrefs.DeleteKey(StatName("gameFinishedOnNormal"));
        PlayerPrefs.DeleteKey(StatName("levelsFinishedOnNormal"));
        PlayerPrefs.DeleteKey(StatName("goldMedalsEarned"));
        PlayerPrefs.DeleteKey(StatName("extraHonorsEarned"));
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
            l.finishedOnNormal = GetSavedStatBool(id, "bonusStreakDone");
            l.specialFound = GetSavedStatBool(id, "specialFound");
            l.fullPoints = GetSavedStatBool(id, "fullPoints");
        }
        stats.SetBestTime(PlayerPrefs.GetInt(StatName("endlessTime")));
        stats.SetBestPoints(PlayerPrefs.GetInt(StatName("endlessScore")));
        stats.SetBestCollected(PlayerPrefs.GetInt(StatName("endlessItems")));
        stats.SetBestNormalTime(PlayerPrefs.GetInt(StatName("endlessSurvivalTimeOnNormal")));
        
    }
    // get general settings data
    private void LoadSettings()
    {
        // TODO: define default settings!
        // if all values == 0, returns false
        stats.hasCustoms = GetSavedStatBool(0, "hasCustoms");
        if (stats.hasCustoms)
        {
            string colname;
            for (int i = 0; i < 9; i++)
            {
                colname = "color" + i.ToString();
                stats.colors[i].r = (byte)PlayerPrefs.GetInt(StatName(colname + "R"));
                stats.colors[i].b = (byte)PlayerPrefs.GetInt(StatName(colname + "B"));
                stats.colors[i].g = (byte)PlayerPrefs.GetInt(StatName(colname + "G"));
                stats.colors[i].a = (byte)PlayerPrefs.GetInt(StatName(colname + "A"));

            }
        }
        if (PlayerPrefs.HasKey(StatName("masterVol")))
            vol.masterVol = PlayerPrefs.GetFloat(StatName("masterVol"));
        else vol.masterVol = 0.5f;
        if (PlayerPrefs.HasKey(StatName("effectVol")))
            vol.effectVol = PlayerPrefs.GetFloat(StatName("effectVol"));
        else vol.effectVol = 0.5f;
        if (PlayerPrefs.HasKey(StatName("musicVol")))
            vol.musicMaxVol = PlayerPrefs.GetFloat(StatName("musicVol"));
        else vol.musicMaxVol = 0.5f;
    }
    public void LoadKongStats()
    {
        stats.SetGameFinished(PlayerPrefs.GetInt(StatName("gameFinishedOnNormal")));
        stats.SetlevelsFinished(PlayerPrefs.GetInt(StatName("levelsFinishedOnNormal")));
        stats.SetGold(PlayerPrefs.GetInt(StatName("goldMedalsEarned")));
        stats.SetExtras(PlayerPrefs.GetInt(StatName("extraHonorsEarned")));
    }
    public void SetVolume()
    {
        PlayerPrefs.SetFloat(StatName("masterVol"), vol.masterVol);
        PlayerPrefs.SetFloat(StatName("effectVol"), vol.effectVol);
        PlayerPrefs.SetFloat(StatName("musicVol"), vol.musicMaxVol);
        PlayerPrefs.Save();
    }
    // save color settings!! not general anymore...
    public void SetSettings()
    {
        string colname;
        for (int i = 0; i < 9; i++)
        {   
            colname = "color" + i.ToString();
            PlayerPrefs.SetInt(StatName(colname + "R"), stats.colors[i].r);
            PlayerPrefs.SetInt(StatName(colname + "B"), stats.colors[i].b);
            PlayerPrefs.SetInt(StatName(colname + "G"), stats.colors[i].g);
            PlayerPrefs.SetInt(StatName(colname + "A"), stats.colors[i].a);
        }
        SetSavedStatBool(0, "hasCustoms", stats.hasCustoms);
        PlayerPrefs.Save();
    }
    // save all game data. Is this needed at all?
    // TODO: get rid of redundant function calls.
    public void SaveScores()
    {
        foreach (Statistics.levelData d in stats.levels)
        {
            int id = d.levelID;
            SaveLevelScore(id);
        }
        SaveEndlessScore();
        SaveKongStats();
        // general settings data 
        SetSettings();
        SetVolume();
        PlayerPrefs.Save();
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
        SetSavedStatBool(id, "bonusStreakDone", d.finishedOnNormal);
        SetSavedStatBool(id, "specialFound", d.specialFound);
        SetSavedStatBool(id, "fullPoints", d.fullPoints);
    }
    // save Kongregate related stats
    private void SaveKongStats()
    {
        PlayerPrefs.SetInt(StatName("gameFinishedOnNormal"), stats.GetAllFinished());
        PlayerPrefs.SetInt(StatName("levelsFinishedOnNormal"), stats.GetFinishedOnNormalCount());
        PlayerPrefs.SetInt(StatName("goldMedalsEarned"), stats.GetAllGold());
        PlayerPrefs.SetInt(StatName("extraHonorsEarned"), stats.GetAllExtras());        
    }
    public void SaveEndlessScore()
    {
        PlayerPrefs.SetInt(StatName("endlessTime"), stats.GetSecsSurvived());
        PlayerPrefs.SetInt(StatName("endlessScore"), stats.GetBestScore());
        PlayerPrefs.SetInt(StatName("endlessItems"), stats.GetBestCollected());
        PlayerPrefs.SetInt(StatName("endlessSurvivalTimeOnNormal"), stats.GetSecsSurvivedNormal());
        PlayerPrefs.Save();
    }
    // Helper methods for saving/loading scores by level
    private int GetSavedStatInt(int level, string stat)
    {
        return PlayerPrefs.GetInt(StatName(stat, level));        
    }
    private bool GetSavedStatBool(int level, string stat)
    {
        return (PlayerPrefs.GetInt(StatName(stat, level)) != 0);
    }
    private void SetSavedStatInt(int level, string stat, int val)
    {

        PlayerPrefs.SetInt(StatName(stat, level), val);
    }
    private void SetSavedStatBool(int level, string stat, bool val)
    {      
        if (val)
            PlayerPrefs.SetInt(StatName(stat, level), 1);
        else PlayerPrefs.SetInt(StatName(stat, level), 0);        
    }
    // Helper for forming keyname
    // (username) + levelnumber + statistic name
    private string StatName(string stat, int level = 99)
    {
        string statname = stat;
        if (level != 99)
            statname = level.ToString() + statname;
        if (KongregateAPI.Connected)
            statname = KongregateAPI.Username + statname;
        else statname = "player" + statname;
        
        return statname;
    }
}
