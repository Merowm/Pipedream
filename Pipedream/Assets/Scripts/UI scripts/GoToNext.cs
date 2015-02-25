using UnityEngine;
using System.Collections;

public class GoToNext : MonoBehaviour {

    public string SceneToGo;
	
    public void GoToScene()
    {
        Invoke("Go", 0.16f);

    }
    void Go()
    {
        Application.LoadLevel(SceneToGo);
    }
}
