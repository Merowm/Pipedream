using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class LinearPlanet : MonoBehaviour {

    public GameObject info;
    public int levelId;

    Statistics stats;
    EventSystem e;
    Canvas c;
    GameObject instantInfo;
    Vector3 offset;
    Text lvnumber;
    Text lengthInSeconds;
    Text score;
    GameObject[] trophies;

    string level;
    string secs;
    string points;
    int trophy;

	// Use this for initialization
	void Start () {
        stats = GameObject.FindWithTag("statistics").GetComponent<Statistics>();
        e = FindObjectOfType<EventSystem>();
        c = FindObjectOfType<Canvas>();
        offset = new Vector3(Screen.width/2, Screen.height/2, 0);
        trophies = new GameObject[3];
        level = levelId.ToString();
        secs = stats.GetLevelTime(levelId).ToString();
        points = stats.GetlevelHighScore(levelId).ToString();
        trophy = stats.GetLevelTrophy(levelId);
	}
	
	// Update is called once per frame
    //void Update () 
    //{
    //    if (e.IsPointerOverGameObject())
    //    {
    //        if (instantInfo == null)
    //        {
    //            Debug.Log("Here");
    //            instantInfo = Instantiate(info, Input.mousePosition, Quaternion.identity) as GameObject;
    //            instantInfo.transform.SetParent(c.transform, false);
    //            instantInfo.transform.localPosition = Input.mousePosition;
    //            SetLevelInfo(instantInfo);
                
    //        }
    //        else
    //        {
    //            instantInfo.transform.localPosition = Input.mousePosition - offset;                
    //        }
    //    }
    //    else
    //    {
    //        if (instantInfo != null)
    //            Destroy(instantInfo);
    //    }
    //}

    public void ShowInfo()
    {
        if (instantInfo == null)
        {
            Debug.Log("Here");
            instantInfo = Instantiate(info, Input.mousePosition, Quaternion.identity) as GameObject;
            instantInfo.transform.SetParent(c.transform, false);
            instantInfo.transform.localPosition = Input.mousePosition - offset;
            SetLevelInfo(instantInfo);

        }
        else
        {
            instantInfo.transform.localPosition = Input.mousePosition - offset;
        }
    }

    public void DestroyInfo()
    {
        if (instantInfo != null)
            Destroy(instantInfo);
    }
    void SetLevelInfo(GameObject panel)
    {
        lvnumber = panel.transform.Find("levelnum").GetComponent<Text>();
        lengthInSeconds = panel.transform.Find("seconds").GetComponent<Text>();
        score = panel.transform.Find("score").GetComponent<Text>();
        trophies[0] = panel.transform.Find("trophy/gold").gameObject;
        trophies[1] = panel.transform.Find("trophy/silver").gameObject;
        trophies[2] = panel.transform.Find("trophy/bronze").gameObject;
        foreach (GameObject t in trophies)
        {
            t.SetActive(false);
        }

        lvnumber.text = "race # " + level;
        lengthInSeconds.text = "racing time " + secs + " seconds";
        score.text = "highest score: " + points + " points";
        if (trophy > 0)
        {
            trophies[trophy - 1].SetActive(true);
        }
    }
}
