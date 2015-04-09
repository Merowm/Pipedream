using UnityEngine;
using System.Collections;

public class LinearLevelSelect : MonoBehaviour {

    // TODO: Define manually!
    public GameObject[] planetButtons;

    Statistics stats;
    LoadScreen loader;
    struct planet
    {
        public int planetnumber;
        public GameObject planetObject;
        public bool isAvailable;
    }

    planet[] planets;
    string levelname;
	// Use this for initialization
	void Start () 
    {
        stats = GameObject.FindWithTag("statistics").GetComponent<Statistics>();
        loader = GameObject.FindWithTag("statistics").GetComponent<LoadScreen>();
        // TODO: sync array length with amount of existing levels in Statistics!
        // NB! First planet is zero? (starting point - no level data, always available?)
        planets = new planet[planetButtons.Length];
        //planets[0] = new planet { planetnumber = 0, planetObject = planetButtons[0], isAvailable = true };
        // debugging:
        stats.UnlockLevel(1);

        for (int i = 0; i < planets.Length; i++)
        {
            planets[i] = new planet { 
                planetnumber = i + 1, 
                planetObject = planetButtons[i], 
                isAvailable = stats.GetAvailability(i + 1) };
        }
        foreach (planet p in planets)
        {
            if (!p.isAvailable)
            {
                p.planetObject.GetComponent<CanvasRenderer>().SetColor(Color.grey);
            }
        }
	}

    // parameter has to be set by hand...
    // TODO: Set by script if time allows (and if REALLY necessary...for maintaining reasons)
	public void GoToPlanetRun(int planetId)
    {

        if (planets[planetId - 1].isAvailable)
        {
            levelname = stats.GetLevelNameAsString(planetId);
            
            loader.showLoader(levelname, false);
            Invoke("Go", 0.16f);
        }
    }
    void Go()
    {
            Application.LoadLevel(levelname);
    }
}
