using UnityEngine;
using System.Collections;

public class GoToNext : MonoBehaviour {

    public string SceneToGo;
    public bool SceneIsMenu;
    LoadScreen loader;
    VolControl sounds;
    void Awake()
    {
        loader = GameObject.FindWithTag("statistics").GetComponent<LoadScreen>();
        sounds = GameObject.FindWithTag("statistics").GetComponent<VolControl>();
    }
    public void GoToScene()
    {
        Debug.Log("invoking Go...");
        Debug.Log("timescale: " + Time.timeScale);
        Go();
        sounds.PlayButtonEffect();
        //Invoke("Go", 0.4f);
    }
    public void Go()
    {        
        Debug.Log("loader invoked!");
        loader.showLoader(SceneToGo, SceneIsMenu);        
        Application.LoadLevel(SceneToGo);
    }
}
