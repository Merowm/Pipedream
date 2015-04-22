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
        Go();
        sounds.PlayButtonEffect();
    }
    public void Go()
    {        
        loader.showLoader(SceneToGo, SceneIsMenu);
        sounds.isInMenu = SceneIsMenu;
        Application.LoadLevel(SceneToGo);
    }
}
