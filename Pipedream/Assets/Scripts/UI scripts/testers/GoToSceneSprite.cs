using UnityEngine;
using System.Collections;

public class GoToSceneSprite : MonoBehaviour
{
   
    LevelTimer lt;
    public int IdOfSceneToGo;

    void Awake()
    {
        
        lt = FindObjectOfType<LevelTimer>();
    }
    void OnMouseUpAsButton()
    {
        
        lt.FinalLevelScore();
        Application.LoadLevel("MenuScene");
    }
}