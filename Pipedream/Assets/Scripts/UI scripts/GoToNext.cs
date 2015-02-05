using UnityEngine;
using System.Collections;

public class GoToNext : MonoBehaviour {

    public int nextSceneInBuild;
	
    void OnMouseUpAsButton()
    {
        Application.LoadLevel(nextSceneInBuild);
    }
}
