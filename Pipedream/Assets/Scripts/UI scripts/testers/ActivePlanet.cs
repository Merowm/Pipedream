using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ActivePlanet : MonoBehaviour {

    Statistics stats;
    LevelSelect select;
    public bool isCurrentPlanet;

    public GameObject[] availableRuns;
    // Use this for initialization
    void Start()
    {
        stats = GameObject.FindWithTag("statistics").GetComponent<Statistics>();
        select = Object.FindObjectOfType<LevelSelect>();        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject[] GetAvailable()
    {
        return availableRuns;
    }

    public void StartRun()
    {
        GameObject from = select.GetCurrent();
        int run = select.GetLevelNumber(from, this.gameObject);
        Application.LoadLevel(stats.GetLevelNameAsString(run));
    }
}
