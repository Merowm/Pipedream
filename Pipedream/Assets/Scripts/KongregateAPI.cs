using UnityEngine;
using System.Collections;

public class KongregateAPI : MonoBehaviour {

    //if the game is connected to Kongregate API
    public static bool Connected = false; 

    //Kongregate user info
    public static int UserID = 0;
    public static string Username = "Guest";
    public static string GameAuthToken = "";

    void Awake(){
#if UNITY_WEBPLAYER
        //make object persistent throughout game 
        DontDestroyOnLoad(gameObject);

        //connect to Kongregate API
        Connect();
#else
        Destroy(gameObject);
#endif
    }

    //function to connect to Kongregate API
    public void Connect()
    {
        Debug.Log("Trying to connect to Kongregate API");
        //if game is not connected to Kongregate API
        if (!Connected) {
            //connect to API on web player, then call OnKongregateAPILoaded, passing in Kongregate user info
            Application.ExternalEval(
                "if(typeof(kongregateUnitySupport) != 'undefined') {" + 
                "kongregateUnitySupport.initAPI('" + gameObject.name + "', 'OnKongregateAPILoaded');" + 
                "}"
                );
        }
        else {
        
        }
    }

    //Called when connected to Kongregate API
    private void OnKongregateAPILoaded(string userInfoString){
        Debug.Log("Connected to Kongregate API");
        Connected = true;
        string[] parameters = userInfoString.Split('|');
        UserID = System.Convert.ToInt32(parameters [0]);
        Username = parameters [1];
        GameAuthToken = parameters [2];

        //set listener to listen for user signing in to Kongregate. Calls OnKongregateUserSignedIn
        Application.ExternalEval("kongregate.services.addEventListener('login', function(){" +
            "var services = kongregate.services;" +
            "var params=[services.getUserId(), services.getUsername(), services.getGameAuthToken()].join('|');" +
            "kongregateUnitySupport.getUnityObject().SendMessage('KongregateObject', 'OnKongregateUserSignedIn', params);");
    }

    //Called when user signs in to Kongregate
    void OnKongregateUserSignedIn(string userInfoString){
        string[] parameters = userInfoString.Split('|');
        UserID = System.Convert.ToInt32(parameters [0]);
        Username = parameters [1];
        GameAuthToken = parameters [2];
    }

    //function to submit data into Kongregate statistics (data can only be non-negative integers)
    public static void SubmitData(string dataName, int dataValue){
        Debug.Log("Trying to submit data to Kongregate");
        if (Connected && Username != "Guest")
        {
            Application.ExternalCall("kongregate.stats.submit", dataName, dataValue);
            Debug.Log(dataName + ": " + dataValue + " has been submitted");
        }
    }

}
