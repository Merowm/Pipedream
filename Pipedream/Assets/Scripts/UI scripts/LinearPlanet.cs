using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class LinearPlanet : MonoBehaviour
{

    public GameObject info;
    public GameObject noGoText;
    public int levelId;

    Statistics stats;
    Canvas mainCanvas;
    GameObject instantInfo;
    GameObject instantNoGo;
    RectTransform infoRect;
    Vector3 offset;
    Text lvnumber;
    Text lengthInSeconds;
    Text score;
    GameObject[] trophies;
    GoToNext goScript;
    GameObject lockPic;

    Vector2 anchor;

    string level;
    string secs;
    string points;
    int trophy;
    bool hasTried;

    // for android
    Vector3 lastTouchPos;

    void Start()
    {
        stats = GameObject.FindWithTag("statistics").GetComponent<Statistics>();

        Canvas[] all = FindObjectsOfType<Canvas>();
        foreach (Canvas c in all)
        {
            if (c.tag == "gameLevelUI")
                mainCanvas = c;
        }

        offset = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        anchor = new Vector2(0, 0);
        trophies = new GameObject[3];
        level = levelId.ToString();
        goScript = this.gameObject.GetComponent<GoToNext>();
        goScript.SceneToGo = stats.GetLevelNameAsString(levelId);
        hasTried = false;
        lockPic = GameObject.Find("locked" + levelId);
        if (stats.GetAvailability(levelId))
            lockPic.SetActive(false);
    }
    public void StartLevel()
    {
        if (instantInfo)
        {
            if (stats.GetAvailability(levelId))
            {
                goScript.GoToScene();
            }
            else
            {
                DestroyInfo();

                instantNoGo = Instantiate(noGoText, this.transform.position, Quaternion.identity) as GameObject;
                instantNoGo.transform.SetParent(this.transform);
                instantNoGo.transform.localScale = new Vector3(1, 1, 1);
                instantNoGo.transform.localPosition = this.transform.position;
                hasTried = true;
            }
        }
    }

    void Update()
    {
        if (instantInfo != null && hasTried == false)
        {
            Vector3 pos = new Vector3(0,0,0);
#if UNITY_ANDROID
                       if (Input.touchCount > 0) 
             { 
                 pos = Input.touches[0].position; 
             } 
#else
            pos = (Input.mousePosition);
#endif

            pos.z = 0;
            if (pos.x > Screen.width * 2 / 3)
            {
                anchor.x = 1.1f;
            }
            else
            {
                anchor.x = -0.1f;
            }
            if (pos.y > Screen.height / 2)
            {
                anchor.y = 1.1f;
            }
            else
            {
                anchor.y = -0.1f;
            }
            infoRect.pivot = anchor;

            infoRect.anchoredPosition = pos;
        }
    }

    public void ShowInfo()
    {
        if (!hasTried)
        {
            if (instantInfo == null)
            {
#if UNITY_ANDROID 
            if (Input.touchCount > 0) 
            { 
                instantInfo = Instantiate(info, Input.touches[0].position, Quaternion.identity) as GameObject;
                instantInfo.transform.localPosition = new Vector3(Input.touches[0].position.x - offset.x, Input.touches[0].position.y - offset.y, offset.z); 
            } 
#else

                instantInfo = Instantiate(info, Input.mousePosition, Quaternion.identity) as GameObject;
                //instantInfo.transform.localPosition = Input.mousePosition - offset;
#endif
                instantInfo.transform.SetParent(mainCanvas.transform, false);
                SetLevelInfo(instantInfo);
                infoRect = instantInfo.GetComponent<RectTransform>();
            }
            else
            {
#if UNITY_ANDROID
#else

                instantInfo.transform.localPosition = Input.mousePosition - offset;
#endif
            }
        }
    }
    public void LeavePlanet()
    {
        DestroyInfo();
        hasTried = false;
    }

    public void DestroyInfo()
    {
        if (instantInfo != null)
            Destroy(instantInfo);
    }
    void SetLevelInfo(GameObject panel)
    {
        secs = stats.GetLevelTime(levelId).ToString();
        points = stats.GetlevelHighScore(levelId).ToString();
        trophy = stats.GetLevelTrophy(levelId);
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
        lengthInSeconds.text = secs + " secs";
        score.text = points;
        if (trophy > 0)
        {
            trophies[trophy - 1].SetActive(true);
        }
    }

    public GameObject GetInstantInfo()
    {
        return instantInfo;
    }
}
