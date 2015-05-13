﻿using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayServices : MonoBehaviour {

    public bool connected = false;

    public enum ACHIEVEMENT_TYPE
    {
        GOLD,
        TOTAL
    }

    [System.Serializable]
    public class ACHIEVEMENT
    {
        public int level;
        public ACHIEVEMENT_TYPE type;
        public string id;
    }

    public ACHIEVEMENT[] achievementData;

    void Start()
    {
#if UNITY_ANDROID
        DontDestroyOnLoad(gameObject);     
        Initialize();
#else
        Destroy(gameObject);
#endif
    }

    void Initialize()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        //.EnableSavedGames()
        // registers a callback to handle game invitations received while the game is not running.
        //.WithInvitationDelegate(<callback method>)
        // registers a callback for turn based match notifications received while the
        // game is not running.
        //.WithMatchDelegate(<callback method>)
        .Build();
        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        SignIn();
    }

    void SignIn()
    {  
        Social.localUser.Authenticate((bool success) => {
        // handle success or failure
            if (success)
            {
                connected = true;
                Social.ShowAchievementsUI();
                Application.LoadLevel("StartMenu");
            }
            else
            {
                Handheld.Vibrate();
            }
        });
    }

    string FindID(int level, ACHIEVEMENT_TYPE type)
    {
        //search through data base for ID
        for (int i = 0; i < achievementData.Length; ++i)
        {
            if (achievementData[i].level == level &&
                achievementData[i].type == type)
            {
                return achievementData[i].id;
            }
        }
        return "";
    }

    public bool UnlockAchievement(int level, ACHIEVEMENT_TYPE type)
    {
        string id = FindID(level, type);
        if (id == "")
        {
            return false;
        }
        Social.ReportProgress(id, 100.0f, (bool success) =>
        {
            // handle success or failure
        });
        return true;
    }

}
