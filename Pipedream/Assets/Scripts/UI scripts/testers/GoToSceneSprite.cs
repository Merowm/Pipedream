using UnityEngine;
using System.Collections;

public class GoToSceneSprite : MonoBehaviour
{
    CountScore sc;
    LevelTimer lt;
    public int IdOfSceneToGo;

    void Awake()
    {
        sc = FindObjectOfType<CountScore>();
        lt = FindObjectOfType<LevelTimer>();
    }
    void OnMouseUpAsButton()
    {
        lt.FinalTimeBonus();
        sc.FinalLevelScore();
        Application.LoadLevel("MenuScene");
    }
}