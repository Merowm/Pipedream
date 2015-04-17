using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LinearLevelSelect : MonoBehaviour
{

    public GameObject noGoText;
    Statistics stats;
    LoadScreen loader;
    GameObject[] buttons;
    GameObject instantNoGo;

    struct planet
    {
        public int planetnumber;
        public GameObject planetObject;
        public bool isAvailable;
        public GameObject lockMark;
        public Vector3 pos;
    }

    planet[] planets;
    string levelname;
    Vector3 offset;

    void Start()
    {
        stats = GameObject.FindWithTag("statistics").GetComponent<Statistics>();
        loader = GameObject.FindWithTag("statistics").GetComponent<LoadScreen>();
        offset = new Vector3(0, 15, 0);

        buttons = GameObject.FindGameObjectsWithTag("planetButton");
        planets = new planet[buttons.Length];

        for (int i = 0; i < planets.Length; i++)
        {
            int levelnum = buttons[i].GetComponent<LinearPlanet>().levelId;
            planets[i] = new planet
            {
                planetObject = buttons[i],
                planetnumber = levelnum,
                lockMark = GameObject.Find("locked" + levelnum),
                pos = buttons[i].GetComponent<RectTransform>().anchoredPosition,
                isAvailable = stats.GetAvailability(levelnum)
            };
        }
        foreach (planet p in planets)
        {
            if (!p.isAvailable)
            {
                p.lockMark.SetActive(true);
            }
            else p.lockMark.SetActive(false);
        }
    }

    public bool GoToPlanetRun(int planetId)
    {
        // Level selection revise!!!!! sry
        // Use FindPlanet for getting right button / level
        // buttons are not necessarily in right order in array!!

        //for android 
        //if instant info not shown, dont go into level

        //if (buttons[planetId - 1].GetComponent<LinearPlanet>().GetInstantInfo() == null) 
        //{ 
        //    Debug.Log(buttons[planetId].GetComponent<LinearPlanet>().GetInstantInfo()); 
        //    return; 
        //} 

        if (planets[FindPlanet(planetId)].isAvailable)
        {
            levelname = stats.GetLevelNameAsString(planetId);

            loader.showLoader(levelname, false);
            Application.LoadLevel(levelname);
            return true;
        }
        else
        {
            Vector3 pos = planets[FindPlanet(planetId)].pos + offset;
            instantNoGo = Instantiate(noGoText, pos, Quaternion.identity) as GameObject;
            instantNoGo.transform.SetParent(this.transform);
            instantNoGo.transform.localScale = new Vector3(1, 1, 1);
            instantNoGo.transform.localPosition = pos;
            return false;
        }
    }
    // for finding right planet in array by id.
    int FindPlanet(int planetId)
    {
        for (int i = 0; i < planets.Length; i++)
        {
            if (planets[i].planetnumber == planetId)
                return i;
        }
        return -1;
    }
}
