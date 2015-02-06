using UnityEngine;
using System.Collections;

public class GoToNext : MonoBehaviour {

    public string SceneToGo;
	
    void OnMouseUpAsButton()
    {
        Application.LoadLevel(SceneToGo);
    }
}
