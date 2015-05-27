using UnityEngine;
using System.Collections;

public class GoToSceneSprite : MonoBehaviour
{
    void Start()
    {
        Invoke("startgame", 2.0f);
    }
    void startgame()
    {        
        Application.LoadLevel("StartMenu");
    }
}