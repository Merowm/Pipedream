using UnityEngine;
using System.Collections;

public class LevelTimer : MonoBehaviour {

    public float timeTimer;
	// Use this for initialization
	void Awake ()
    {
        timeTimer = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeTimer += Time.deltaTime;
	}

    public void FinalTimeBonus()
    {
        // Send final time to CountScore / Statistics?
        
    }
}
