using UnityEngine;
using System.Collections;

public class Joksikin : MonoBehaviour 
{

    public Scenes sceneToLoad;
    public enum Scenes
    {
        proto,
        StartScene
    };
	// Use this for initialization
	void Start () 
    {
        Application.LoadLevel(sceneToLoad.ToString());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
