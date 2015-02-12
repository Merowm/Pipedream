using UnityEngine;
using System.Collections;

public class GoToNext : MonoBehaviour {

    public string SceneToGo;
	
    public void GoToScene()
    {
        Application.LoadLevel(SceneToGo);
    }
}
