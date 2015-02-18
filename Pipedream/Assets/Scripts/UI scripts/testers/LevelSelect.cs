using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

    // Planets set manually in scene (for now). Should be placed in order!
    public GameObject[] planets;
    struct level
    {
        public int levelNumber;
        public GameObject startPoint;
        public GameObject endPoint;
    }
    Statistics stats;
    GameObject currentPlanet;

    level[] levels;

	void Start () 
    {
        stats = GameObject.FindWithTag("statistics").GetComponent<Statistics>();
        levels[0] = new level { levelNumber = 1, startPoint = planets[0], endPoint = planets[1] };
        levels[1] = new level { levelNumber = 2, startPoint = planets[0], endPoint = planets[2] };
        levels[2] = new level { levelNumber = 3, startPoint = planets[2], endPoint = planets[3] };

        // set one, and only one, planet as current
        foreach (GameObject p in planets)
        {
            p.GetComponent<ActivePlanet>().isCurrentPlanet = false;            
        }

        currentPlanet = planets[stats.GetLastPlanet() - 1];
        currentPlanet.GetComponent<ActivePlanet>().isCurrentPlanet = true;
	}	

    // returns 0 if no such level was found.
    public int GetLevelNumber(GameObject current, GameObject destination)
    {
        foreach (level l in levels)
        {
            if ((l.startPoint == current && l.endPoint == destination) || 
                (l.startPoint == destination && l.endPoint == current))
            {
                return l.levelNumber;
            }
        }
        return 0;
    }

    public GameObject GetCurrent()
    {
        foreach (GameObject p in planets)
        {
            if (p.GetComponent<ActivePlanet>().isCurrentPlanet)
                return p;
        }
        return null;
    }
}
