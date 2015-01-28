using UnityEngine;
using System.Collections;

public class GoToSceneSprite : MonoBehaviour
{

    public int IdOfSceneToGo;

    void OnMouseUpAsButton()
    {
        Debug.Log("Click detected");
        Application.LoadLevel(IdOfSceneToGo);
    }
}