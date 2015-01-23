using UnityEngine;
using System.Collections;

// Creates button with OnGUI method for changing scene.
// TODO: Rename script and create all buttons at the same time?
public class GoToScene : MonoBehaviour {

	public int nextScene;
    public Texture2D buttontex;
	void Awake ()
    {
	    
	}
	
    void OnGUI()
    {
        if (GUI.Button(new Rect(30, 30, 100, 100), buttontex))
            Application.LoadLevel(nextScene);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
