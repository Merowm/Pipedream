using UnityEngine;
using System.Collections;

public class GoToSceneSprite : MonoBehaviour
{
    CountScore sc;
    public int IdOfSceneToGo;

    void Awake()
    {
        sc = FindObjectOfType<CountScore>();
    }
    void OnMouseUpAsButton()
    {
        sc.FinalLevelScore();
        Application.LoadLevel("MenuScene");
    }
}