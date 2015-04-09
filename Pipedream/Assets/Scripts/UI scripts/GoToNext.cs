using UnityEngine;
using System.Collections;

public class GoToNext : MonoBehaviour {

    public string SceneToGo;
    public bool SceneIsMenu;
    LoadScreen loader;
    void Awake()
    {
        loader = GameObject.FindWithTag("statistics").GetComponent<LoadScreen>();

    }
    public void GoToScene()
    {
        Debug.Log("loader invoked!");
        loader.showLoader(SceneToGo, SceneIsMenu);
        Go();
        //Invoke("Go", 0.01f);
    }
    void Go()
    {
        
        Application.LoadLevel(SceneToGo);
    }
}
