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
        Debug.Log(1080 *9 / 16);
        
	}
	
	
    void Update () 
    {
        if (instantInfo != null)
        {
            offset = new Vector3(Screen.width * 0.1f, Screen.height * 0.1f, 0);
            Vector3 pos = /*Camera.main.ScreenToWorldPoint*/(Input.mousePosition);
            
            pos.z = 0;
            instantInfo.transform.position = pos;
        }
               CanvasScaler sc = FindObjectOfType<CanvasScaler>(); 
        Debug.Log("input: " + Input.mousePosition);
        Debug.Log(stats.ScreenToCanvasPoint(c, Input.mousePosition));

        
    }

    public void ShowInfo()
    {
        Debug.Log("show info");
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
        Debug.Log("destroy");
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

        lvnumber.text = "run # " + level;
        lengthInSeconds.text = "racing time " + secs + " seconds";
        score.text = "highest score: " + points + " points";
        if (trophy > 0)
        {
            trophies[trophy - 1].SetActive(true);
        }
    }
}
