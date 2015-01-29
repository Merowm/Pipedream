using UnityEngine;
using System.Collections;

public class GoToSceneSprite : MonoBehaviour
{

    public int IdOfSceneToGo;

    void OnMouseUpAsButton()
    {
        
        Application.LoadLevel(IdOfSceneToGo);
    }
}